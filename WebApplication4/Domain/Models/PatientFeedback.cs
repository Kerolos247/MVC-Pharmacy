using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.Models
{
    public enum FeedbackSentiment
    {
        Positive,
        Negative,
        Neutral
    }
    public class PatientFeedback
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; } 

        public string Notes { get; set; }

        public FeedbackSentiment feedbackSentiment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

