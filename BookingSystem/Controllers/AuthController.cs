using BookingSystem.Entities;
using BookingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Services;
using BookingSystem.DTOs;
namespace TravelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/register")]
        public async Task<IActionResult> AdminRegister([FromBody] AdminUserRegistrationRequest newUserRequest)
        {
            if (newUserRequest == null) return BadRequest("Invalid user data.");

            var newUser = new User
            {
                Name = newUserRequest.Name,
                Email = newUserRequest.Email,
                Password = newUserRequest.Password,
                ContactNumber = newUserRequest.ContactNumber,
                Role = string.IsNullOrEmpty(newUserRequest.Role) ? "Customer" : newUserRequest.Role
            };

            await _userRepository.AddUsers(newUser);
            return Ok(new { message = $"User registered successfully as {newUser.Role}" });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest newUserRequest)
        {
            if (newUserRequest == null) return BadRequest("Invalid user data.");

            var newUser = new User
            {
                Name = newUserRequest.Name,
                Email = newUserRequest.Email,
                Password = newUserRequest.Password,
                ContactNumber = newUserRequest.ContactNumber,
                Role = "Customer"
            };

            await _userRepository.AddUsers(newUser);
            return Ok(new { message = "User registered successfully as Customer" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid login request.");
            }

            var user = await _userRepository.ValidateUser(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Ok(new { Token = token, Role = user.Role });
        }
    }
}