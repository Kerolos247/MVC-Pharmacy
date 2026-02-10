// Real-time Search Logic
document.getElementById("searchInput").addEventListener("input", function () {
    const query = this.value.toLowerCase();
    const rows = document.querySelectorAll("#presTable tbody tr");
    rows.forEach(row => {
        const text = row.innerText.toLowerCase();
        row.style.display = text.includes(query) ? "" : "none";
    });
});

// Toast Auto-dismiss Logic
window.addEventListener('load', () => {
    const toasts = document.querySelectorAll('.alert-toast');
    if (toasts.length > 0) {
        setTimeout(() => {
            toasts.forEach(toast => {
                toast.style.opacity = "0";
                toast.style.transform = "translateX(50px)";
                setTimeout(() => toast.remove(), 500);
            });
        }, 5000);
    }
});