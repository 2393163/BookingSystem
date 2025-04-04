using System;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Review
    {
        public int ReviewID { get; set; }
        public long UserID { get; set; }
        public int PackageID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Package? Package { get; set; }
    }
}
