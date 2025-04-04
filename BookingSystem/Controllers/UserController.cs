using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(long id)
        {
            var user = await _userRepository.GetUserByEmail(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User is null.");
            }

            var user = new User
            {
                UserID = newUser.UserID,
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = newUser.Role,
                ContactNumber = newUser.ContactNumber
            };

            await _userRepository.AddUsers(user);
            return Ok(new { message = "User registered successfully as Customer" });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UserDTO updatedUser)
        {
            if (updatedUser == null || updatedUser.UserID != id)
            {
                return BadRequest("User data is invalid.");
            }

            var user = new User
            {
                UserID = updatedUser.UserID,
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                Password = updatedUser.Password,
                Role = updatedUser.Role,
                ContactNumber = updatedUser.ContactNumber
            };

            await _userRepository.UpdateUser(user.UserID,user.Name,user.Email,user.ContactNumber);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userRepository.DeleteUser(id);
            return NoContent();
        }
    }
}
