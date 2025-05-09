using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingSystem.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required(ErrorMessage = "Please provide the UserID.")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "Please provide the PackageID.")]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Please provide a Rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "Comment must not exceed 1000 characters.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please provide the TimeStamp.")]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // New properties for specific reviews
        [Range(1, 5, ErrorMessage = "FoodRating must be between 1 and 5.")]
        public int? FoodReview { get; set; }

        [Range(1, 5, ErrorMessage = "FlightRating must be between 1 and 5.")]
        public int? FlightReview { get; set; }

        [Range(1, 5, ErrorMessage = "HotelRating must be between 1 and 5.")]
        public int? HotelReview { get; set; }

        [Range(1, 5, ErrorMessage = "TravelAgentRating must be between 1 and 5.")]
        public int? TravelAgentReview { get; set; }
        //New properties for specific reviews



        // Navigation properties
        public User? User { get; set; }
        [JsonIgnore]
        public Package? Package { get; set; }
    }
}
