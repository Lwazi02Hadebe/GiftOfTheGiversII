using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }

        public int? UserID { get; set; } // Null for anonymous
        public User? User { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "Credit Card"; // Credit Card, PayPal

        public string? PaymentTransactionID { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Completed, Failed, Pending

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;

        public bool IsAnonymous { get; set; } = false;
    }
}