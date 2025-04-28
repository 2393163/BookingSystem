namespace BookingSystem.DTOs
{
    public class UserRegistrationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
    }
}