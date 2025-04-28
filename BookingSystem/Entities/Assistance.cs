using System;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Assistance
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public long UserID { get; set; } // Removed MaxLength as it is not applicable to numeric types.

        [Required(ErrorMessage = "Please enter the Issue Description.")]
        [MaxLength(5000, ErrorMessage = "The Issue Description must not exceed 5000 characters.")]
        public string IssueDescription { get; set; }

        [Required(ErrorMessage = "Please enter the Status.")]
        [MaxLength(50, ErrorMessage = "The Status must not exceed 50 characters.")]
        public string Status { get; set; } = "Active";

        [Required(ErrorMessage = "Please enter the Resolution Time.")]
        public DateTime? ResolutionTime { get; set; }

        // Navigation properties
        public User? User { get; set; }
    }
}
