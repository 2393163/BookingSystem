using System;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Insurance
    {
        [Key]
        public int InsuranceID { get; set; }
        public long UserID { get; set; }
      //  public long BookingID { get; set; }
        public string CoverageDetails { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; } = "Active";

        // Navigation properties
        public User? User { get; set; }
    }
}
