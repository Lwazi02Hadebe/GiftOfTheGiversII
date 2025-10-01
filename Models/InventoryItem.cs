using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class InventoryItem
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty; // Food, Shelter, Medical

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } = string.Empty; // Bottle, Box, Packet

        public List<ProjectNeed> ProjectNeeds { get; set; } = new();
    }
}