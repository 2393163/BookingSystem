using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public long UserID { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }

        // Navigation properties
        public Booking? Booking { get; set; }
    }
}
