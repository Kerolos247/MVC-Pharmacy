const searchInput = document.getElementById("searchInput");
const tableRows = document.querySelectorAll("#suppliersTable tbody tr");

searchInput.addEventListener("input", function () {
    const filter = this.value.toLowerCase();

    tableRows.forEach(row => {
        const text = row.innerText.toLowerCase();
        row.style.display = text.includes(filter) ? "" : "none";
    });
});