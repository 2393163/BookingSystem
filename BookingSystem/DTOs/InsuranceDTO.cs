using System;

namespace BookingSystem.DTOs
{
    public class InsuranceDTO
    {
        public int InsuranceID { get; set; }
        public long UserID { get; set; }
        public long BookingID { get; set; }
        public string? CoverageDetails { get; set; }
        public string? Provider { get; set; }
        public string? Status { get; set; }

    }
}
