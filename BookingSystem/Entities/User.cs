using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Entities
{
    public class User
    {
        [Key]
        public long UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Admin", "TravelAgent", "Customer"
        [Required]
        public string ContactNumber { get; set; }

        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Insurance>? Insurances { get; set; }
        public ICollection<Assistance>? Assistances { get; set; }

      
    }
}
