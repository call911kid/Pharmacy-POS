using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PrescriptionScanner.Api;
using PrescriptionScanner.Models;
using PrescriptionScanner.Server;
using UI.Theme;

namespace UI.Forms
{
    public class PrescriptionScannerForm : Form
    {
        // ── Left panel ────────────────────────────────────────────────────────
        private PictureBox  picQR = null!;
        private Label       lblUrl = null!;
        private Label       lblStatus = null!;
        private Button      btnScanFile = null!;
        private Button      btnClear = null!;
        private ProgressBar progressBar = null!;

        // ── Right panel ───────────────────────────────────────────────────────
        private Label        lblPatient = null!;
        private Label        lblDoctor = null!;
        private Label        lblDate = null!;
        private DataGridView gridMeds = null!;
        private RichTextBox  rtbNotes = null!;
        private Label        lblCopyNotice = null!;
        private System.Windows.Forms.Timer copyNoticeTimer = null!;
        private readonly ToolTip _copyTip = new();

        // ── Infrastructure ────────────────────────────────────────────────────
        private ScannerServer? _server;
        private const int     Port = 8181;
        private readonly HashSet<string> _productNameKeys = new();
        private readonly Font _matchedMedicineFont = UiPalette.BodyFont(9.5f, FontStyle.Bold);

        // ═════════════════════════════════════════════════════════════════════
        public PrescriptionScannerForm()
        {
            Text          = "Prescription Scanner";
            Size          = new Size(1080, 720);
            MinimumSize   = new Size(900, 620);
            StartPosition = FormStartPosition.CenterScreen;
            Font          = UiPalette.BodyFont(9f);
            BackColor     = UiPalette.AppBackground;

            BuildUI();
            StartServer();
            Shown += async (_, _) => await LoadProductNamesAsync();
        }

        // ═════════════════════════════════════════════════════════════════════
        #region UI Builder

        private void BuildUI()
        {
            Controls.Add(BuildRightPanel());
            Controls.Add(BuildLeftPanel());
            Controls.Add(BuildHeader());
        }

        // ── Header ────────────────────────────────────────────────────────────
        private Panel BuildHeader()
        {
            var panel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 60,
                BackColor = UiPalette.Primary
            };
            panel.Controls.Add(new Label
            {
                Text      = "🩺  Prescription Scanner",
                ForeColor = Color.White,
                Font      = UiPalette.BodyFont(15f, FontStyle.Bold),
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });
            return panel;
        }

        // ── Left Panel ────────────────────────────────────────────────────────
        private Panel BuildLeftPanel()
        {
            var panel = new Panel
            {
                Dock      = DockStyle.Left,
                Width     = 260,
                BackColor = UiPalette.Primary
            };

            var flow = new FlowLayoutPanel
            {
                Dock          = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding       = new Padding(16, 14, 16, 14),
                AutoScroll    = true
            };

            flow.Controls.Add(LeftLabel("Scan QR from your phone:", 11f, FontStyle.Bold));
            flow.Controls.Add(LeftSpacer(6));

            picQR = new PictureBox
            {
                Size     = new Size(220, 220),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor= Color.White,
                Margin   = new Padding(2, 0, 2, 6)
            };
            flow.Controls.Add(picQR);

            lblUrl = new Label
            {
                Text      = "Starting server…",
                ForeColor = UiPalette.PrimaryMuted,
                AutoSize  = false,
                Width     = 224,
                Height    = 34,
                TextAlign = ContentAlignment.MiddleCenter,
                Font      = new Font("Segoe UI", 8f),
                Margin    = new Padding(0, 0, 0, 8)
            };
            flow.Controls.Add(lblUrl);

            flow.Controls.Add(new Label
            {
                Text      = "──────  or  ──────",
                ForeColor = UiPalette.PrimaryMuted,
                AutoSize  = false,
                Width     = 224,
                Height    = 22,
                TextAlign = ContentAlignment.MiddleCenter,
                Font      = new Font("Segoe UI", 8f),
                Margin    = new Padding(0, 0, 0, 8)
            });

            btnScanFile = LeftButton("📂  Open Image from Disk",
                UiPalette.PrimaryMuted, UiPalette.TextPrimary);
            btnScanFile.Click += BtnScanFile_Click;
            flow.Controls.Add(btnScanFile);
            flow.Controls.Add(LeftSpacer(6));

            btnClear = LeftButton("🗑  Clear Results",
                UiPalette.RowAlternate, UiPalette.TextPrimary);
            btnClear.Click += (s, e) => ClearResults();
            flow.Controls.Add(btnClear);
            flow.Controls.Add(LeftSpacer(10));

            progressBar = new ProgressBar
            {
                Width   = 224,
                Height  = 6,
                Style   = ProgressBarStyle.Marquee,
                Visible = false,
                Margin  = new Padding(0, 0, 0, 6)
            };
            flow.Controls.Add(progressBar);

            lblStatus = new Label
            {
                Text      = "⏳  Waiting for image…",
                ForeColor = UiPalette.PrimaryMuted,
                AutoSize  = false,
                Width     = 224,
                Height    = 44,
                TextAlign = ContentAlignment.MiddleCenter,
                Font      = new Font("Segoe UI", 9f, FontStyle.Italic)
            };
            flow.Controls.Add(lblStatus);

            panel.Controls.Add(flow);
            return panel;
        }

        // ── Right Panel ───────────────────────────────────────────────────────
        private Panel BuildRightPanel()
        {
            var panel = new Panel
            {
                Dock    = DockStyle.Fill,
                Padding = new Padding(18, 14, 18, 14)
            };

            // Patient info strip
            var infoPanel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 78,
                BackColor = UiPalette.PrimaryMuted,
                Padding   = new Padding(14, 10, 14, 10)
            };
            infoPanel.Paint += (s, e) =>
            {
                using var pen = new Pen(UiPalette.Border, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, infoPanel.Width - 1, infoPanel.Height - 1);
            };

            lblPatient = InfoLabel("Patient: —", FontStyle.Bold);
            lblDoctor  = InfoLabel("Doctor: —");
            lblDate    = InfoLabel("Date: —");

            var infoFlow = new FlowLayoutPanel
            {
                Dock          = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding       = new Padding(0)
            };
            infoFlow.Controls.AddRange(new Control[] { lblPatient, lblDoctor, lblDate });
            infoPanel.Controls.Add(infoFlow);

            // Notes
            rtbNotes = new RichTextBox
            {
                Dock        = DockStyle.Top,
                Height      = 76,
                BackColor   = UiPalette.RowAlternate,
                ForeColor   = UiPalette.TextPrimary,
                Font        = UiPalette.BodyFont(9f),
                BorderStyle = BorderStyle.None,
                ReadOnly    = true
            };

            var lblNotesTitle = new Label
            {
                Text      = "📋  Additional Notes:",
                Dock      = DockStyle.Top,
                Height    = 26,
                Font      = UiPalette.BodyFont(9.5f, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary
            };

            // Medicines grid
            gridMeds = new DataGridView
            {
                Dock                = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible   = false,
                AllowUserToAddRows  = false,
                ReadOnly            = true,
                BackgroundColor     = UiPalette.Surface,
                BorderStyle         = BorderStyle.None,
                SelectionMode       = DataGridViewSelectionMode.CellSelect,
                MultiSelect         = false,
                ClipboardCopyMode   = DataGridViewClipboardCopyMode.EnableWithoutHeaderText,
                Font                = UiPalette.BodyFont(9.5f),
                GridColor           = UiPalette.Border
            };

            var hStyle = gridMeds.ColumnHeadersDefaultCellStyle;
            hStyle.BackColor = UiPalette.Primary;
            hStyle.ForeColor = Color.White;
            hStyle.Font      = UiPalette.BodyFont(9.5f, FontStyle.Bold);

            gridMeds.DefaultCellStyle.SelectionBackColor       = UiPalette.PrimaryMuted;
            gridMeds.DefaultCellStyle.SelectionForeColor       = UiPalette.TextPrimary;
            gridMeds.AlternatingRowsDefaultCellStyle.BackColor = UiPalette.RowAlternate;

            gridMeds.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name",         HeaderText = "Medicine",      FillWeight = 25 });
            gridMeds.Columns.Add(new DataGridViewTextBoxColumn { Name = "Dosage",        HeaderText = "Dosage",        FillWeight = 20 });
            gridMeds.Columns.Add(new DataGridViewTextBoxColumn { Name = "Frequency",     HeaderText = "Frequency",     FillWeight = 30 });
            gridMeds.Columns.Add(new DataGridViewTextBoxColumn { Name = "Instructions",  HeaderText = "Instructions",  FillWeight = 25 });
            gridMeds.KeyDown += GridMeds_KeyDown;
            gridMeds.CellClick += GridMeds_CellClick;
            gridMeds.CellDoubleClick += GridMeds_CellDoubleClick;

            var lblMedsTitle = new Label
            {
                Text      = "💊  Prescribed Medicines:",
                Dock      = DockStyle.Top,
                Height    = 26,
                Font      = UiPalette.BodyFont(9.5f, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary
            };

            lblCopyNotice = new Label
            {
                Text = "Copied medicine name",
                AutoSize = true,
                Visible = false,
                Font = UiPalette.BodyFont(9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                BackColor = Color.FromArgb(230, 255, 238),
                Padding = new Padding(8, 4, 8, 4)
            };
            copyNoticeTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            copyNoticeTimer.Tick += (_, _) =>
            {
                lblCopyNotice.Visible = false;
                copyNoticeTimer.Stop();
            };

            // Assemble (reverse Dock order)
            panel.Controls.Add(gridMeds);
            panel.Controls.Add(lblCopyNotice);
            panel.Controls.Add(lblMedsTitle);
            panel.Controls.Add(rtbNotes);
            panel.Controls.Add(lblNotesTitle);
            panel.Controls.Add(infoPanel);
            panel.Resize += (_, _) =>
            {
                lblCopyNotice.Location = new Point(
                    Math.Max(8, panel.ClientSize.Width - lblCopyNotice.Width - 14),
                    6);
            };

            return panel;
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private static Label LeftLabel(string text, float size = 9f, FontStyle style = FontStyle.Regular)
            => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = UiPalette.BodyFont(size, style) };

        private static Label LeftSpacer(int height)
            => new Label { Height = height, Width = 224, AutoSize = false };

        private static Button LeftButton(string text, Color back, Color fore)
        {
            var btn = new Button
            {
                Text      = text,
                Width     = 224,
                Height    = 40,
                BackColor = back,
                ForeColor = fore,
                FlatStyle = FlatStyle.Flat,
                Font      = UiPalette.BodyFont(9.5f, FontStyle.Bold),
                Cursor    = Cursors.Hand,
                Margin    = new Padding(0, 0, 0, 4)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private static Label InfoLabel(string text, FontStyle style = FontStyle.Regular)
            => new Label
            {
                Text      = text,
                AutoSize  = true,
                Font      = UiPalette.BodyFont(9.5f, style),
                ForeColor = UiPalette.TextPrimary,
                Margin    = new Padding(0, 1, 0, 1)
            };

        private void GridMeds_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedMedicineName();
                e.Handled = true;
            }
        }

        private void GridMeds_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CopySelectedMedicineName();
            }
        }

        private void GridMeds_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridMeds.Columns[e.ColumnIndex].Name == "Name")
            {
                CopySelectedMedicineName(e.RowIndex, e.ColumnIndex);
            }
        }

        private void CopySelectedMedicineName()
        {
            if (gridMeds.CurrentRow?.Cells["Name"].Value is string name && !string.IsNullOrWhiteSpace(name))
            {
                Clipboard.SetText(name);
                SetStatus("Copied medicine name", Color.FromArgb(22, 163, 74));
                ShowCopyNotice(name);
            }
        }

        private void CopySelectedMedicineName(int rowIndex, int columnIndex)
        {
            if (gridMeds.Rows[rowIndex].Cells[columnIndex].Value is string name && !string.IsNullOrWhiteSpace(name))
            {
                Clipboard.SetText(name);
                SetStatus("Copied medicine name", Color.FromArgb(22, 163, 74));
                ShowCopyNotice(name);

                var cellRect = gridMeds.GetCellDisplayRectangle(columnIndex, rowIndex, true);
                var point = new Point(cellRect.Right + 6, cellRect.Top + 4);
                _copyTip.Show($"Copied: {name}", gridMeds, point, 1200);
            }
        }

        private void ShowCopyNotice(string medicineName)
        {
            lblCopyNotice.Text = $"Copied: {medicineName}";
            var parent = lblCopyNotice.Parent;
            if (parent is not null)
            {
                lblCopyNotice.Location = new Point(
                    Math.Max(8, parent.ClientSize.Width - lblCopyNotice.Width - 14),
                    6);
            }
            lblCopyNotice.Visible = true;
            lblCopyNotice.BringToFront();
            copyNoticeTimer.Stop();
            copyNoticeTimer.Start();
        }

        #endregion

        // ═════════════════════════════════════════════════════════════════════
        #region Server

        /// <summary>Opens Windows Firewall for our port so the phone can connect.</summary>
        private static void OpenFirewallPort(int port)
        {
            try
            {
                // Remove old rule first (ignore errors), then add fresh one
                var del = new System.Diagnostics.ProcessStartInfo
                {
                    FileName        = "netsh",
                    Arguments       = $"advfirewall firewall delete rule name=\"PrescriptionScanner-{port}\"",
                    CreateNoWindow  = true,
                    UseShellExecute = false
                };
                System.Diagnostics.Process.Start(del)?.WaitForExit(3000);

                var add = new System.Diagnostics.ProcessStartInfo
                {
                    FileName        = "netsh",
                    Arguments       = $"advfirewall firewall add rule name=\"PrescriptionScanner-{port}\" " +
                                      $"dir=in action=allow protocol=TCP localport={port}",
                    CreateNoWindow  = true,
                    UseShellExecute = false
                };
                System.Diagnostics.Process.Start(add)?.WaitForExit(3000);
            }
            catch { /* non-critical — user can allow manually */ }
        }

        private void StartServer()
        {
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "prescription-scanner.html");

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show(
                    "wwwroot\\prescription-scanner.html not found in output folder.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string html = File.ReadAllText(htmlPath);
            string myIP = NetworkHelper.GetLocalIP();
            string url  = $"http://{myIP}:{Port}";

            // Replace analyzeImage: send image to desktop, poll /result, display it
            string hook = $@"
<script>
(function() {{
  window.analyzeImage = async function() {{
    if (!window._b64) return;
    document.getElementById('loadingOverlay').classList.add('active');
    document.getElementById('errorBox').classList.remove('visible');
    document.getElementById('analyzeBtn').disabled = true;
    try {{
      var up = await fetch('http://{myIP}:{Port}/upload', {{
        method:'POST', headers:{{'Content-Type':'application/json'}},
        body: JSON.stringify({{image: window._b64}})
      }});
      if (!up.ok) throw new Error('upload failed');
      var result = null;
      for (var i = 0; i < 60; i++) {{
        await new Promise(r => setTimeout(r, 1000));
        var res = await fetch('http://{myIP}:{Port}/result');
        if (res.ok) {{ var j = await res.json(); if (j.ready) {{ result = j.data; break; }} }}
      }}
      if (result) displayResult(result);
      else        showError();
    }} catch(e) {{ showError(); }}
    finally {{
      document.getElementById('loadingOverlay').classList.remove('active');
      document.getElementById('analyzeBtn').disabled = false;
    }}
  }};
}})();
</script>";

            html = html.Replace("</body>", hook + "\n</body>");

            OpenFirewallPort(Port);

            _server = new ScannerServer(html, Port);
            _server.OnImageReceived += base64 =>
                BeginInvoke(new Action(async () => await AnalyzeAndDisplayAsync(base64)));
            _server.Start();

            // Export certificate so user can install it on mobile if needed
            string certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "certificate.crt");
            _server.ExportCertificate(certPath);

            picQR.Image = QRHelper.Generate(url);
            lblUrl.Text = url;
        }

        #endregion

        // ═════════════════════════════════════════════════════════════════════
        #region Event handlers

        private async void BtnScanFile_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title  = "Select Prescription Image",
                Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.webp"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string base64 = Convert.ToBase64String(File.ReadAllBytes(dlg.FileName));
            await AnalyzeAndDisplayAsync(base64);
        }

        #endregion

        // ═════════════════════════════════════════════════════════════════════
        #region Analysis

        private async Task AnalyzeAndDisplayAsync(string base64)
        {
            SetStatus("🔍  Analysing…", UiPalette.TextMuted);
            progressBar.Visible = true;
            btnScanFile.Enabled = false;

            try
            {
                var result = await ApiClient.AnalyzeAsync(base64);
                if (result != null)
                {
                    ShowResults(result);
                    // Send result to mobile so it can display it too
                    _server?.SetResult(JsonConvert.SerializeObject(new {
                        patient_name    = result.PatientName,
                        doctor_name     = result.DoctorName,
                        date            = result.Date,
                        additional_notes= result.Notes,
                        medicines       = result.Medicines == null ? new object[0] :
                            System.Array.ConvertAll(result.Medicines, m => (object)new {
                                name         = m.Name,
                                dosage       = m.Dosage,
                                frequency    = m.Frequency,
                                instructions = m.Instructions
                            })
                    }));
                }
                else
                    SetStatus("❌  Analysis failed. Try again.", Color.IndianRed);
            }
            catch (Exception ex)
            {
                SetStatus($"❌  Error: {ex.Message}", Color.IndianRed);
            }
            finally
            {
                progressBar.Visible = false;
                btnScanFile.Enabled = true;
            }
        }

        private void ShowResults(PrescriptionData d)
        {
            lblPatient.Text = $"Patient: {d.PatientName ?? "—"}";
            lblDoctor.Text  = $"Doctor: {d.DoctorName   ?? "—"}";
            lblDate.Text    = $"Date: {d.Date            ?? "—"}";
            rtbNotes.Text   = d.Notes ?? "—";

            gridMeds.Rows.Clear();
            foreach (var m in d.Medicines ?? Array.Empty<Medicine>())
            {
                string name = Clean(m.Name);
                gridMeds.Rows.Add(name, Clean(m.Dosage), Clean(m.Frequency), Clean(m.Instructions));
            }

            int matchedCount = HighlightMatchedMedicines();
            SetStatus($"✅  Done — {d.Medicines?.Length ?? 0} medicine(s) found | matched in DB: {matchedCount}", UiPalette.Primary);
        }

        private void ClearResults()
        {
            gridMeds.Rows.Clear();
            lblPatient.Text = "Patient: —";
            lblDoctor.Text  = "Doctor: —";
            lblDate.Text    = "Date: —";
            rtbNotes.Text   = "";
            SetStatus("⏳  Waiting for image…", UiPalette.TextMuted);
        }

        private void SetStatus(string msg, Color color)
        {
            lblStatus.Text      = msg;
            lblStatus.ForeColor = color;
        }

        private static string Clean(string? val)
            => string.IsNullOrEmpty(val) || val == "Not specified" ? "—" : val;

        private async Task LoadProductNamesAsync()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();

                string? connectionString = config.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    return;
                }

                var options = new DbContextOptionsBuilder<PharmacyDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                await using var context = new PharmacyDbContext(options);
                var names = await context.Products
                    .AsNoTracking()
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name))
                    .Select(p => p.Name)
                    .ToListAsync();

                _productNameKeys.Clear();
                foreach (var name in names)
                {
                    _productNameKeys.Add(NormalizeDrugName(name));
                }

                if (gridMeds.Rows.Count > 0)
                {
                    HighlightMatchedMedicines();
                }
            }
            catch
            {
                // Keep scanner functional even if DB is unavailable.
            }
        }

        private int HighlightMatchedMedicines()
        {
            if (_productNameKeys.Count == 0)
            {
                return 0;
            }

            int matchedCount = 0;
            foreach (DataGridViewRow row in gridMeds.Rows)
            {
                if (row.Cells["Name"].Value is not string name || string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                bool exists = _productNameKeys.Contains(NormalizeDrugName(name));
                var nameCell = row.Cells["Name"];

                if (exists)
                {
                    matchedCount++;
                    nameCell.Style.ForeColor = Color.FromArgb(22, 163, 74);
                    nameCell.Style.SelectionForeColor = Color.FromArgb(22, 163, 74);
                    nameCell.Style.Font = _matchedMedicineFont;
                }
                else
                {
                    nameCell.Style.ForeColor = UiPalette.TextPrimary;
                    nameCell.Style.SelectionForeColor = UiPalette.TextPrimary;
                    nameCell.Style.Font = gridMeds.Font;
                }
            }

            return matchedCount;
        }

        private static string NormalizeDrugName(string name)
        {
            var chars = name
                .Trim()
                .ToLowerInvariant()
                .Where(char.IsLetterOrDigit)
                .ToArray();
            return new string(chars);
        }

        #endregion

        // ═════════════════════════════════════════════════════════════════════
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            copyNoticeTimer?.Stop();
            copyNoticeTimer?.Dispose();
            _matchedMedicineFont.Dispose();
            _server?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
