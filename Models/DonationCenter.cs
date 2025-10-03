using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class DonationCenter
    {
        [Key]
        public int CenterID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string OperatingHours { get; set; } = "Mon-Fri 9:00-17:00";

        public bool IsActive { get; set; } = true;

        public List<AcceptedItem> AcceptedItems { get; set; } = new();
    }

    public class AcceptedItem
    {
        [Key]
        public int AcceptedItemID { get; set; }

        [Required]
        public int CenterID { get; set; }
        public DonationCenter? Center { get; set; }

        [Required]
        public int ItemID { get; set; }
        public InventoryItem? Item { get; set; }

        public bool IsCurrentlyAccepted { get; set; } = true;

        [StringLength(500)]
        public string? SpecialInstructions { get; set; }
    }
}
