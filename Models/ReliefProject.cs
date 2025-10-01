using GiftOfTheGiversII.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class ReliefProject
    {
        [Key]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(255)]
        public string ProjectName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Location { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; }

        [Required]
        public string Status { get; set; } = "Planning"; // Active, Completed, Planning

        public string ImageUrl { get; set; } = "/images/default-project.jpg";

        public List<ProjectNeed> ProjectNeeds { get; set; } = new();
    }
}