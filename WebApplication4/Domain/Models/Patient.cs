namespace WebApplication4.Domain.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Relations
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
