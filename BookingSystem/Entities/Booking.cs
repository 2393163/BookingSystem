using System;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public long UserID { get; set; }  // Ensure this matches the type in User.cs
        public int PackageID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public int PaymentID { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Package? Package { get; set; }
        public Payment? Payment { get; set; }
    }
}

