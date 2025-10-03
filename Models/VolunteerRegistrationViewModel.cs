using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGiversII.Models
{
    public class VolunteerRegistrationViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = string.Empty;

        [Display(Name = "Skills (comma separated)")]
        public string? Skills { get; set; }

        [Display(Name = "Availability")]
        public string? Availability { get; set; }

        [Display(Name = "I have my own transportation")]
        public bool HasTransportation { get; set; }

        [Display(Name = "Emergency Contact Information")]
        public string? EmergencyContact { get; set; }
    }
}