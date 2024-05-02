const btn = document.getElementById("copyBtn");

btn.addEventListener("click", e => {
    navigator.clipboard.writeText(window.location.href);
})