namespace WebApplication4.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }



        // Relations
        public ICollection<Medicine> Medicines { get; set; }
    }
}
