using BookingSystem.Entities;

namespace BookingSystem.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
