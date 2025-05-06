namespace BookingSystem.DTOs
{
    public class PackageDTO
    {
        public int PackageID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public long Price { get; set; }
        public string IncludedServices { get; set; }
        public string Category { get; set; }
        public int Travelagent { get; set; }
        public string image { get; set; }
    }
}
