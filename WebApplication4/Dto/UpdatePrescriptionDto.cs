namespace WebApplication4.Dto
{
    public class UpdatePrescriptionDto
    {
        public DateTime? Date { get; set; }

        public string? Notes { get; set; }

        public int? PatientId { get; set; }

        public string? PharmacistId { get; set; }
    }
}
