using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceController(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddInsurance([FromBody] InsuranceDTO insuranceDto)
        {
            if (insuranceDto == null)
            {
                return BadRequest("Insurance data is null.");
            }

            try
            {
                var insurance = await _insuranceRepository.AddInsuranceAsync(insuranceDto.UserID, insuranceDto.BookingID);

                var result = new InsuranceDTO
                {
                    InsuranceID = insurance.InsuranceID,
                    UserID = insurance.UserID,
                    BookingID = insurance.BookingID,
                    CoverageDetails = insurance.CoverageDetails,
                    Provider = insurance.Provider,
                    Status = insurance.Status
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        ////[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<InsuranceDTO>>> GetAllInsurances()
        {
            var insurances = await _insuranceRepository.GetAllInsurancesAsync();
            var result = insurances.Select(i => new InsuranceDTO
            {
                InsuranceID = (int)i.InsuranceID,
                UserID = i.UserID,
                CoverageDetails = i.CoverageDetails,
                Provider = i.Provider,
                Status = i.Status
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<InsuranceDTO>> GetInsuranceById(long id)
        {
            var insurances = await _insuranceRepository.GetAllInsurancesAsync();
            var insurance = insurances.FirstOrDefault(i => i.InsuranceID == id);

            if (insurance == null)
            {
                return NotFound("Insurance not found.");
            }

            var result = new InsuranceDTO
            {
                InsuranceID = (int)insurance.InsuranceID,
                UserID = insurance.UserID,
                CoverageDetails = insurance.CoverageDetails,
                Provider = insurance.Provider,
                Status = insurance.Status
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInsuranceStatus(long id, [FromBody] InsuranceDTO insuranceDto)
        {
            if (insuranceDto == null || insuranceDto.InsuranceID != id)
            {
                return BadRequest("Invalid insurance data.");
            }

            var updatedInsurance = await _insuranceRepository.UpdateInsuranceStatusAsync(id, insuranceDto.Status);
            if (updatedInsurance == null)
            {
                return NotFound("Insurance not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInsurance(long id)
        {
            await _insuranceRepository.DeleteInsuranceAsync((int)id);
            return NoContent();
        }
    }
}
