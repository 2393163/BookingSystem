namespace BookingSystem.DTOs
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public long UserID { get; set; }
        public int PackageID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
