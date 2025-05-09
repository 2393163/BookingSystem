using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using BookingSystem.Entities;


namespace BookingSystem.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // Unique User ID
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role), // Assigns Role-based Claims
            };

            var payload = new Dictionary<string, object>
            {
                { "UserID", user.UserID }, // Unique User ID
                { "Email", user.Email },   // User Email
                { "Role", user.Role } ,
                {"Name",user.Name },
                {"ContactNumber",user.ContactNumber }// User Role directly in the payload
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2), // Token valid for 2 hours
                signingCredentials: credentials
            );

            foreach (var item in payload)
            {
                token.Payload[item.Key] = item.Value;
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
