namespace BookingSystem.DTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public long UserID { get; set; }
        public int BookingID { get; set; }
        public long Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
    }
}
