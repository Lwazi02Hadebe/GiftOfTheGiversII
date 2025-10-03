using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerID { get; set; }

        [Required]
        public int UserID { get; set; }
        public User? User { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Skills { get; set; } // Comma-separated skills

        [StringLength(50)]
        public string? Availability { get; set; } // Weekdays, Weekends, Anytime

        public bool HasTransportation { get; set; }

        [StringLength(1000)]
        public string? EmergencyContact { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Suspended

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovalDate { get; set; }

        public List<VolunteerTask>? VolunteerTasks { get; set; }
    }
}