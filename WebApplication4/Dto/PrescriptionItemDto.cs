using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Dto
{
    public class PrescriptionItemDto
    {
        [Required(ErrorMessage = "Medicine is required")]
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
