// Search Logic
const searchInput = document.getElementById("searchInput");
const tableRows = document.querySelectorAll("#inventoryTable tbody tr");

searchInput.addEventListener("keyup", function () {
    const filter = this.value.toLowerCase();
    tableRows.forEach(row => {
        const text = row.innerText.toLowerCase();
        row.style.display = text.includes(filter) ? "" : "none";
    });
});

// Dynamic Status Badges Logic
document.addEventListener("DOMContentLoaded", function () {
    const rows = document.querySelectorAll("#inventoryTable tbody tr");

    rows.forEach(row => {
        const quantityCell = row.querySelector(".quantity-cell");
        const statusCell = row.querySelector(".status-cell");
        const quantity = parseInt(quantityCell.dataset.quantity);

        if (quantity === 0) {
            statusCell.innerHTML = '<span class="status-badge status-out">Out of Stock</span>';
            quantityCell.style.color = 'var(--danger)';
        }
        else if (quantity < 10) {
            statusCell.innerHTML = '<span class="status-badge status-low">Low Stock</span>';
            quantityCell.style.color = 'var(--warning)';
        }
        else {
            statusCell.innerHTML = '<span class="status-badge status-ok">In Stock</span>';
            quantityCell.style.color = 'var(--primary)';
        }
    });
});