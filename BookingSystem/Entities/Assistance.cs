using System;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities;

namespace BookingSystem.Entities
{
    public class Assistance
    {
        [Key]
        public int RequestID { get; set; }
        public long UserID { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime ResolutionTime { get; set; }

        // Navigation properties
        public User? User { get; set; }
    }
}
