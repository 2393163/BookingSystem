using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Booking
    {
        [Key]
        public long BookingID { get; set; }
        [Required(ErrorMessage = "Please provide the UserID.")]
        public long UserID { get; set; } // Removed [MaxLength] since it's not applicable to numeric types.

        [Required(ErrorMessage = "Please provide the PackageID.")]
        public int PackageID { get; set; } // Removed [MaxLength] as it is not valid for integer types.

        [Required(ErrorMessage = "Please provide the Start Date.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please provide the End Date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please provide the Status.")]
        [MaxLength(100, ErrorMessage = "The Status must not exceed 100 characters.")]
        public string Status { get; set; } = "Booked";

        [Required(ErrorMessage = "Please provide the PaymentID.")]
        public long PaymentID { get; set; } // Removed [MaxLength] since it's not applicable to numeric types.

        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

        // Navigation properties
        public User? User { get; set; }
        [JsonIgnore]
        public Package? Package { get; set; }
        public Payment? Payment { get; set; }
    }
}

