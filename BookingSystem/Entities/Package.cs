using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Entities
{
    public class Package
    {
        [Key]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Please enter the Title")]
        [MaxLength(5000, ErrorMessage = "The Title must not exceed 5000 characters")]
        public string Title { get; set; }

        [MaxLength(5000, ErrorMessage = "The Description must not exceed 5000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the Duration")]
        public int Duration { get; set; }  // Removed MaxLength

        [Required(ErrorMessage = "Please enter the Price")]
        public long Price { get; set; }  // Removed MaxLength

        [MaxLength(5000, ErrorMessage = "The Included Services must not exceed 5000 characters")]
        public string IncludedServices { get; set; }

        [Required(ErrorMessage = "Please enter the Category")]
        [MaxLength(5000, ErrorMessage = "The Category must not exceed 5000 characters")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter the TravelAgentNumber")]
        public int Travelagent { get; set; }
        

        public string image { get; set; }

        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
