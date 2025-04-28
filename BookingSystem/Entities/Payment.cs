using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Entities
{
    public class Payment
    {
        
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "Please provide the UserID.")]
        public long UserID { get; set; } // Removed [MaxLength] as it's not valid for numeric types.

        [Required(ErrorMessage = "Please provide the BookingID.")]
        public long BookingID { get; set; } // Ensures BookingID is always present.

        [Required(ErrorMessage = "Please provide the Payment Amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; } // Validates positive payment amounts.

        [Required(ErrorMessage = "Please provide the Payment Status.")]
        [MaxLength(50, ErrorMessage = "Status must not exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please provide the Payment Method.")]
        [MaxLength(100, ErrorMessage = "Payment Method must not exceed 100 characters.")]
        public string PaymentMethod { get; set; }

        // Navigation properties
        public Booking? Booking { get; set; }
    }
}
