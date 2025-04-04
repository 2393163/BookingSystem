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
    public class PackageController : ControllerBase
    {
        private readonly PackageRepository _packageRepository;

        public PackageController(PackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            return Ok(await _packageRepository.GetAllPackagesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            var package = await _packageRepository.GetPackageByPackageIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] PackageDTO newPackage)
        {
            if (newPackage == null)
            {
                return BadRequest("Package is null.");
            }

            var package = new Package
            {
                PackageID = newPackage.PackageID,
                Title = newPackage.Title,
                Description = newPackage.Description,
                Duration = newPackage.Duration,
                Price = newPackage.Price,
                IncludedServices = newPackage.IncludedServices,
                Category = newPackage.Category
            };

            await _packageRepository.AddPackagesAsync(package);
            return Ok(package);       
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageDTO updatedPackage)
        {
            if (updatedPackage == null || updatedPackage.PackageID != id)
            {
                return BadRequest("Package data is invalid.");
            }

            var package = new Package
            {
                PackageID = updatedPackage.PackageID,
                Title = updatedPackage.Title,
                Description = updatedPackage.Description,
                Duration = updatedPackage.Duration,
                Price = updatedPackage.Price,
                IncludedServices = updatedPackage.IncludedServices,
                Category = updatedPackage.Category
            };

            await _packageRepository.UpdatePackageAsync(package.PackageID,package.Title,package.Description,package.Duration,package.Price,package.IncludedServices);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageRepository.DeletePackageAsync(id);
            return NoContent();
        }
    }
}
