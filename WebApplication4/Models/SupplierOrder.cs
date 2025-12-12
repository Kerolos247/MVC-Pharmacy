namespace WebApplication4.Models
{
    public class SupplierOrder
    {
        public int SupplierOrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<SupplierOrderItem> Items { get; set; }
    }
}
