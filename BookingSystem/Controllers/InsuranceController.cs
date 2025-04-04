using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.DTOs;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly InsuranceRepository _insuranceRepository;

        public InsuranceController()
        {
            _insuranceRepository = new InsuranceRepository();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddInsurance([FromBody] InsuranceDTO newInsurance)
        {
            if (newInsurance == null)
            {
                return BadRequest("Insurance is null.");
            }

            var insurance = new Insurance
            {
                InsuranceID = newInsurance.InsuranceID,
                UserID = newInsurance.UserID,
                CoverageDetails = newInsurance.CoverageDetails,
                Provider=newInsurance.Provider,
                Status=newInsurance.Status
            };

            await _insuranceRepository.AddInsuranceAsync(insurance);
            return Ok(newInsurance);
        }
        [HttpGet]
        public async Task<ActionResult<List<Insurance>>> GetAllInsurances()
        {
            var insurances = await _insuranceRepository.GetAllInsurancesAsync();
            return Ok(insurances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Insurance>> GetInsuranceById(int id)
        {
            var insurance = await _insuranceRepository.GetAllInsurancesAsync();
            var insuranceItem = insurance.FirstOrDefault(i => i.InsuranceID == id);

            if (insuranceItem == null)
            {
                return NotFound();
            }

            return Ok(insuranceItem);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInsurance(int id, [FromBody] InsuranceDTO updatedInsurance)
        {
            if (updatedInsurance == null || updatedInsurance.InsuranceID != id)
            {
                return BadRequest("Insurance data is invalid.");
            }

            var insurance = new Insurance
            {
                InsuranceID = updatedInsurance.InsuranceID,
                UserID = updatedInsurance.UserID,
                CoverageDetails = updatedInsurance.CoverageDetails,
            };

            await _insuranceRepository.UpdateInsuranceAsync(insurance.InsuranceID,insurance.CoverageDetails);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
        {
            await _insuranceRepository.DeleteInsuranceAsync(id);
            return NoContent();
        }
    }
}
