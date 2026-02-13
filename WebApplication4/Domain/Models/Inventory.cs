namespace WebApplication4.Domain.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }

        // FK
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
