using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class VolunteerAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        public int VolunteerID { get; set; }
        public Volunteer? Volunteer { get; set; }

        [Required]
        public int TaskID { get; set; }
        public VolunteerTask? Task { get; set; }

        public DateTime AssignmentDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Assigned"; // Assigned, Completed, Cancelled, NoShow

        public DateTime? CompletedDate { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; } // 1-5 stars

        [StringLength(1000)]
        public string? Feedback { get; set; }

        [Range(0, 24)]
        public decimal? HoursCompleted { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
