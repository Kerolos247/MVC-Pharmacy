using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class UpdateMedcineDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DosageForm { get; set; }
        public string? Strength { get; set; }
        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
        public int? supplierId { get; set; }
    }
}
