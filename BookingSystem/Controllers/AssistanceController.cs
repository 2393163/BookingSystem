using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssistanceController : ControllerBase // Inherit from ControllerBase for HTTP methods
    {
        private readonly IAssistanceRepository _assistanceRepository;

        public AssistanceController(IAssistanceRepository assistanceRepository)
        {
            _assistanceRepository = assistanceRepository ?? throw new ArgumentNullException(nameof(assistanceRepository));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RequestAssistance([FromBody] AssistanceDTO newAssistance)
        {
            if (newAssistance == null)
            {
                return BadRequest("Assistance request is null.");
            }

            var assistance = new Assistance
            {
                RequestID = newAssistance.RequestID,
                UserID = newAssistance.UserID,
                Status = newAssistance.Status,
                IssueDescription = newAssistance.IssueDescription,
                ResolutionTime = newAssistance.ResolutionTime
            };

            await _assistanceRepository.AddAssistanceRequestAsync(assistance);
            return CreatedAtAction(nameof(GetAssistanceById), new { id = assistance.RequestID }, assistance);
        }

        [HttpGet]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> GetAllAssistances()
        {
            var assistances = await _assistanceRepository.GetAllAssistanceRequestsAsync();
            return Ok(assistances);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAssistanceById(int id)
        {
            var assistanceItem = (await _assistanceRepository.GetAllAssistanceRequestsAsync())
                .FirstOrDefault(a => a.RequestID == id);

            if (assistanceItem == null)
            {
                return NotFound();
            }

            return Ok(assistanceItem);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> UpdateAssistance(int id, [FromBody] AssistanceDTO updatedAssistance)
        {
            if (updatedAssistance == null || updatedAssistance.RequestID != id)
            {
                return BadRequest("Assistance data is invalid.");
            }

            await _assistanceRepository.UpdateAssistanceRequestAsync(id, updatedAssistance.IssueDescription, updatedAssistance.Status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> DeleteAssistance(int id)
        {
            await _assistanceRepository.DeleteAssistanceRequestAsync(id);
            return NoContent();
        }
    }
}
