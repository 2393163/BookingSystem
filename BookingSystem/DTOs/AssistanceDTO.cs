namespace BookingSystem.DTOs
{
    public class AssistanceDTO
    {
        public int RequestID { get; set; }
        public long UserID { get; set; }
        public string Status { get; set; }
        public string IssueDescription { get; set; }
        public DateTime ResolutionTime { get; set; }

    }
}
