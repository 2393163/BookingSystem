using System.Collections.Generic;

namespace BookingSystem.DTOs
{
    public class UserDTO
    {
        public long UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ContactNumber { get; set; }
    }
}
