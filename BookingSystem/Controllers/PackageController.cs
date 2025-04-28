using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;

        public PackageController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPackages()
        {
            return Ok(await _packageRepository.GetAllPackagesAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPackage(int id)
        {
            var package = await _packageRepository.GetPackageByPackageIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Travel Agent")]
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
        [Authorize(Roles = "Travel Agent, Admin")]

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

            await _packageRepository.UpdatePackageAsync(package.PackageID, package.Title, package.Description, package.Duration, package.Price, package.IncludedServices);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageRepository.DeletePackageAsync(id);
            return NoContent();
        }
        // Method to fetch packages based on the number of bookings
        [HttpGet("popular")]
        [Authorize]
        public async Task<IActionResult> GetPackagesByBookings()
        {
            var packages = await _packageRepository.GetPackagesByBookingsAsync();
            return Ok(packages.OrderByDescending(p => p.Bookings.Count)); // Assumes Bookings is populated
        }

        // Method to fetch packages based on average rating
        [HttpGet("rating")]
        [Authorize]
        public async Task<IActionResult> GetPackagesByRating()
        {
            var packages = await
            _packageRepository.GetPackagesByRatingAsync();
            return Ok(packages);
        }



        // Method to fetch packages based on recent reviews
        [HttpGet("reviews/recent")]
        [Authorize]
        public async Task<IActionResult> GetPackagesByRecentReviews()
        {
            var packages = await _packageRepository.GetPackagesByRecentReviewsAsync();

            var recentPackages = packages.Select(p => new
            {
                Package = p,
                LatestReviewTime = p.Reviews.Any() ? p.Reviews.Max(r => r.TimeStamp) : DateTime.MinValue // Handle empty reviews
            }).OrderByDescending(r => r.LatestReviewTime);

            return Ok(recentPackages.Select(r => r.Package));
        }
        // Get packages by review count
        [HttpGet("highest-review")]
        public async Task<IActionResult> GetPackagesByReviewCount()
        {
            var packages = await _packageRepository.GetPackagesByReviewCountAsync();
            return Ok(packages);
        }

    }
}