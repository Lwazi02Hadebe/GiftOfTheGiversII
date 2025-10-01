using GiftOfTheGiversII.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class ProjectNeed
    {
        [Key]
        public int ProjectNeedID { get; set; }

        [Required]
        public int ProjectID { get; set; }
        public ReliefProject? Project { get; set; }

        [Required]
        public int ItemID { get; set; }
        public InventoryItem? Item { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityNeeded { get; set; }

        public int QuantityFulfilled { get; set; } = 0;
    }
}
