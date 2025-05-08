namespace BookingSystem.DTOs
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public long UserID { get; set; }
        public int PackageID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime TimeStamp { get; set; }

        //New properties for specific reviews

        public int? FoodReview { get; set; }
        public int? FlightReview { get; set; }
        public int? HotelReview { get; set; }
        public int? TravelAgentReview { get; set; }

    }
}
