using GiftOfTheGiversII.Models;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; } = "Donor"; // Donor, Volunteer, Admin

        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;

        public bool IsEmailVerified { get; set; } = false;

        public List<Donation> Donations { get; set; } = new();
    }
}
