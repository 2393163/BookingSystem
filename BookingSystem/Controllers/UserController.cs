using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] //by admin
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] //by admin

        public async Task<ActionResult> GetUser(long id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UserDTO updatedUser)
        {
            var loggedInUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(loggedInUserIdClaim))
            {
                return Unauthorized("User ID claim not found");
            }

            if (!long.TryParse(loggedInUserIdClaim, out var loggedInUserId))
            {
                return Unauthorized("Invalid User ID claim");
            }

            if (User.IsInRole("Admin") || loggedInUserId == id)
            {
                await _userRepository.UpdateUser(updatedUser.UserID,updatedUser.Name,updatedUser.Email,updatedUser.ContactNumber);
                return Ok("User updated successfully");
            }

            return Forbid();
        }

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userRepository.DeleteUser(id);
            return NoContent();
        }



        [Authorize(Roles = "Admin")] //only admin
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Invalid user data");

            var users = await _userRepository.GetUsersByName(name);
            if (users == null || users.Count == 0)
                return NotFound($"No users found with name '{name}'");

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/total-users")]
        public async Task<IActionResult> GetTotalUsers()
        {
            var totalUsers = await _userRepository.GetTotalUsers();
            return Ok(new { TotalUsers = totalUsers });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/users-by-role")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string role)
        {
            if (string.IsNullOrEmpty(role)) return BadRequest("Role is required.");

            var users = await _userRepository.GetUsersByRole(role);
            return Ok(users);
        }

    }
}