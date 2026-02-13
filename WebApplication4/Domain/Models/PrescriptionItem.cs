namespace WebApplication4.Domain.Models
{
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; }

        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int Duration { get; set; }

        // FK Medicine
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        // FK Prescription
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
    }
}
