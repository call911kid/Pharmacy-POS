const API_URL = "/api/barcode";
let html5QrCode;
let lastScannedCode = "";

function startScanner() {
  document.getElementById("startBtn").style.display = "none";
  document.getElementById("stopBtn").style.display = "flex";

  html5QrCode = new Html5Qrcode("reader");
  const config = {
    fps: 10,
    qrbox: { width: 250, height: 150 },
    aspectRatio: 1.0,
  };

  html5QrCode
    .start({ facingMode: "environment" }, config, onScanSuccess)
    .catch((err) => {
      alert("Camera error: " + err);
      stopScanner();
    });
}

function stopScanner() {
  if (html5QrCode) {
    html5QrCode.stop().then(() => {
      document.getElementById("startBtn").style.display = "flex";
      document.getElementById("stopBtn").style.display = "none";
      html5QrCode.clear();
    });
  }
}

async function onScanSuccess(decodedText) {
  if (decodedText === lastScannedCode) return;
  lastScannedCode = decodedText;

  try {
    const ctx = new (window.AudioContext || window.webkitAudioContext)();
    const osc = ctx.createOscillator();
    osc.frequency.value = 800;
    osc.connect(ctx.destination);
    osc.start();
    osc.stop(ctx.currentTime + 0.1);
  } catch (e) {}

  document.getElementById("last-result").textContent = decodedText;
  const statusEl = document.getElementById("api-status");
  statusEl.className = "api-status pending";
  statusEl.textContent = "Sending to API...";

  try {
    const res = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        barcode: decodedText,
        timestamp: new Date().toISOString(),
      }),
    });

    const data = await res.json();

    if (data.success) {
      statusEl.className = "api-status ok";
      statusEl.textContent = "Sent Successfully";
      addToHistory(decodedText, data.timestamp);
    } else {
      statusEl.className = "api-status err";
      statusEl.textContent = "Failed to Send";
    }
  } catch (e) {
    statusEl.className = "api-status err";
    statusEl.textContent = "No Connection to Desktop";
  }
}

function addToHistory(code, time) {
  const list = document.getElementById("history-list");
  const emptyState = list.querySelector(".empty-state");
  if (emptyState) emptyState.remove();

  const item = document.createElement("div");
  item.className = "history-item";
  item.innerHTML = `
                <div>
                    <div class="history-code">${code}</div>
                    <div class="history-time">${time}</div>
                </div>
                <button class="btn btn-outline" style="padding: 4px 8px; font-size: 11px; width: auto;" onclick="copyCode(this, '${code}')">Copy</button>
            `;
  list.insertBefore(item, list.firstChild);
}

async function clearHistory() {
  await fetch("/api/clear", { method: "POST" });
  document.getElementById("history-list").innerHTML = `
                <div class="empty-state">
                    <i class="ph ph-barcode" style="font-size: 32px; opacity: 0.5; margin-bottom: 8px;"></i>
                    <p>No barcodes scanned yet</p>
                </div>
            `;
  document.getElementById("last-result").textContent = "— None —";
  document.getElementById("api-status").className = "api-status pending";
  document.getElementById("api-status").textContent = "Waiting for scan...";
}

function copyCode(btn, code) {
  navigator.clipboard.writeText(code).then(() => {
    const originalText = btn.innerText;
    btn.innerText = "Copied!";
    setTimeout(() => (btn.innerText = originalText), 2000);
  });
}

function resetLastScan() {
  lastScannedCode = "";

  document.getElementById("last-result").textContent = "— None —";
  const statusEl = document.getElementById("api-status");
  statusEl.className = "api-status pending";
  statusEl.textContent = "Ready to scan again...";
}
