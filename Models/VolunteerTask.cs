using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class VolunteerTask
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public int ProjectID { get; set; }
        public ReliefProject? Project { get; set; }

        [StringLength(50)]
        public string TaskType { get; set; } = "General"; // Distribution, Medical, Logistics, etc.

        [Required]
        public string Location { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(1, 100)]
        public int VolunteersNeeded { get; set; } = 1;

        public int VolunteersAssigned { get; set; } = 0;

        public string Status { get; set; } = "Open"; // Open, InProgress, Completed, Cancelled

        [StringLength(500)]
        public string? Requirements { get; set; }

        public string? SpecialInstructions { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public List<VolunteerAssignment>? VolunteerAssignments { get; set; }
    }
}
