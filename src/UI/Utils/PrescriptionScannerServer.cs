using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace PrescriptionScanner.Server
{
    public class ScannerServer : IDisposable
    {
        private TcpListener? _listener;
        private Thread? _thread;
        private bool _running;

        private readonly string _htmlContent;
        private readonly int _port;

        private string? _pendingResult;
        private readonly object _resultLock = new object();

        public event Action<string>? OnImageReceived;

        public ScannerServer(string htmlContent, int port = 8181)
        {
            _htmlContent = htmlContent ?? throw new ArgumentNullException(nameof(htmlContent));
            _port = port;
        }

        public void ExportCertificate(string path) { }

        public void SetResult(string resultJson)
        {
            lock (_resultLock) { _pendingResult = resultJson; }
        }

        public void Start()
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _running = true;
            _thread = new Thread(Listen) { IsBackground = true, Name = "ScannerServer" };
            _thread.Start();
        }

        private void Listen()
        {
            while (_running)
            {
                try
                {
                    if (_listener is null)
                    {
                        break;
                    }

                    var client = _listener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(_ => HandleClient(client));
                }
                catch when (!_running) { break; }
                catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Accept: {ex.Message}"); }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                client.ReceiveTimeout = 30_000;
                client.SendTimeout = 30_000;
                using var stream = client.GetStream();
                using var reader = new StreamReader(stream, Encoding.UTF8, false, 8192, leaveOpen: true);
                using var writer = new StreamWriter(stream, new UTF8Encoding(false), 8192, leaveOpen: true) { AutoFlush = true };
                HandleRequest(reader, writer, stream);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Client: {ex.Message}"); }
            finally { try { client.Close(); } catch { } }
        }

        private void HandleRequest(StreamReader reader, StreamWriter writer, Stream raw)
        {
            try
            {
                string? requestLine = reader.ReadLine();
                if (string.IsNullOrEmpty(requestLine)) return;

                var m = Regex.Match(requestLine, @"^(\w+)\s+(\S+)\s+HTTP/\d\.\d$");
                if (!m.Success) return;

                string method = m.Groups[1].Value;
                string path = m.Groups[2].Value;

                var headers = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                string? line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    var h = Regex.Match(line!, @"^([^:]+):\s*(.+)$");
                    if (h.Success) headers[h.Groups[1].Value] = h.Groups[2].Value;
                }

                if (method == "OPTIONS") Respond(writer, raw, 200, "OK", null, null);
                else if (method == "GET" && path == "/") ServeHtml(writer, raw);
                else if (method == "GET" && path == "/result") ServeResult(writer, raw);
                else if (method == "POST" && path == "/upload") ReceiveImage(headers, reader, writer, raw);
                else Respond(writer, raw, 404, "Not Found", null, null);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Request: {ex.Message}"); }
        }

        private void ServeResult(StreamWriter writer, Stream raw)
        {
            string json;
            lock (_resultLock)
            {
                if (_pendingResult == null)
                {
                    json = "{\"ready\":false}";
                }
                else
                {
                    json = "{\"ready\":true,\"data\":" + _pendingResult + "}";
                    _pendingResult = null;
                }
            }
            Respond(writer, raw, 200, "OK", "application/json", Encoding.UTF8.GetBytes(json));
        }

        private void Respond(StreamWriter writer, Stream raw, int code, string msg, string? contentType, byte[]? body)
        {
            writer.WriteLine($"HTTP/1.1 {code} {msg}");
            writer.WriteLine("Access-Control-Allow-Origin: *");
            writer.WriteLine("Access-Control-Allow-Methods: GET, POST, OPTIONS");
            writer.WriteLine("Access-Control-Allow-Headers: Content-Type");
            writer.WriteLine("Connection: close");
            if (contentType != null) writer.WriteLine($"Content-Type: {contentType}");
            writer.WriteLine($"Content-Length: {body?.Length ?? 0}");
            writer.WriteLine();
            writer.Flush();
            if (body != null && body.Length > 0) { raw.Write(body, 0, body.Length); raw.Flush(); }
        }

        private void ServeHtml(StreamWriter writer, Stream raw)
            => Respond(writer, raw, 200, "OK", "text/html; charset=utf-8", Encoding.UTF8.GetBytes(_htmlContent));

        private void ReceiveImage(
            System.Collections.Generic.Dictionary<string, string> headers,
            StreamReader reader, StreamWriter writer, Stream raw)
        {
            int maxLen = 5 * 1024 * 1024;
            if (headers.TryGetValue("Content-Length", out var ls) && int.TryParse(ls, out int p))
                maxLen = Math.Min(p, maxLen);

            var sb = new StringBuilder();
            var buf = new char[8192];
            int read = 0;
            while (read < maxLen)
            {
                int got = reader.Read(buf, 0, Math.Min(buf.Length, maxLen - read));
                if (got <= 0) break;
                sb.Append(buf, 0, got);
                read += got;
            }

            string? base64 = ExtractBase64(sb.ToString());
            if (!string.IsNullOrEmpty(base64))
            {
                lock (_resultLock) { _pendingResult = null; }
                OnImageReceived?.Invoke(base64);
                Respond(writer, raw, 200, "OK", "application/json", Encoding.UTF8.GetBytes("{\"success\":true}"));
            }
            else
            {
                Respond(writer, raw, 400, "Bad Request", null, null);
            }
        }

        private static string? ExtractBase64(string json)
        {
            try
            {
                int k = json.IndexOf("\"image\""); if (k < 0) return null;
                int q1 = json.IndexOf('"', k + 7) + 1;
                int q2 = json.IndexOf('"', q1);
                return json.Substring(q1, q2 - q1);
            }
            catch { return null; }
        }

        public void Dispose()
        {
            _running = false;
            try { _listener?.Stop(); } catch { }
            try { _thread?.Join(300); } catch { }
        }
    }
}
