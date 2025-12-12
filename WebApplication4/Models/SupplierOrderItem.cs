namespace WebApplication4.Models
{
    public class SupplierOrderItem
    {
        public int SupplierOrderItemId { get; set; }
        public int Quantity { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int SupplierOrderId { get; set; }
        public SupplierOrder SupplierOrder { get; set; }
    }
}
