using System;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Insurance
    {
        [Key]
        public int InsuranceID { get; set; }
        [Required(ErrorMessage = "Please provide the UserID.")]
        public long UserID { get; set; } // Removed unused [MaxLength] as it's not valid for numeric types.

        [Required(ErrorMessage = "Please provide the BookingID.")] // Uncomment if BookingID is necessary
        public long BookingID { get; set; }

        [Required(ErrorMessage = "Please provide the Coverage Details.")]
        [MaxLength(500, ErrorMessage = "Coverage Details must not exceed 500 characters.")]
        public string? CoverageDetails { get; set; }

        [Required(ErrorMessage = "Please provide the Provider.")]
        [MaxLength(100, ErrorMessage = "Provider name must not exceed 100 characters.")]
        public string? Provider { get; set; }

        [Required(ErrorMessage = "Please provide the Status.")]
        [MaxLength(50, ErrorMessage = "Status must not exceed 50 characters.")]
        public string? Status { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Booking Booking { get; set; }

    }
}
