// Smooth Search Logic
const searchInput = document.getElementById("searchInput");
const tableRows = document.querySelectorAll("#patientsTable tbody tr");

searchInput.addEventListener("keyup", function () {
    const filter = this.value.toLowerCase();

    tableRows.forEach(row => {
        const content = row.innerText.toLowerCase();
        row.style.display = content.includes(filter) ? "" : "none";

        // إضافة تأثير ظهور ناعم
        if (content.includes(filter)) {
            row.style.animation = "fadeIn 0.3s ease forwards";
        }
    });
});