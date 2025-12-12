namespace WebApplication4.Models
{
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; }

        public string Dosage { get; set; }       // 1 tablet
        public string Frequency { get; set; }    // twice daily
        public int Duration { get; set; }        // 5 days

        // FK Medicine
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        // FK Prescription
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
    }
}
