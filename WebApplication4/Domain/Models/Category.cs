namespace WebApplication4.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Relations
        public ICollection<Medicine> Medicines { get; set; }
    }
}
