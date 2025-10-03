using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class ResourceDonation
    {
        [Key]
        public int ResourceDonationID { get; set; }

        public int? UserID { get; set; }
        public User? User { get; set; }

        [Required]
        public int ItemID { get; set; }
        public InventoryItem? Item { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public int? ProjectID { get; set; }
        public ReliefProject? Project { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Received, Distributed

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ReceivedDate { get; set; }

        public bool IsAnonymous { get; set; } = false;

        [StringLength(255)]
        public string? DeliveryMethod { get; set; } // Drop-off, Pickup, Shipping
    }
}
