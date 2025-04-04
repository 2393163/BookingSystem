using System;

namespace BookingSystem.DTOs
{
    public class BookingDTO
    {
        public int BookingID { get; set; }
        public long UserID { get; set; }
        public int PackageID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int PaymentID { get; set; }
    }
}
