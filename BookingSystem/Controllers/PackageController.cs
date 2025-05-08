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
        //[Authorize]
        public async Task<IActionResult> GetPackages()
        {
            return Ok(await _packageRepository.GetAllPackagesAsync());
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetPackage(int id)
        {
            var package = await _packageRepository.GetPackageByPackageIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }
        [HttpGet("search/title")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByTitleAsync([FromQuery] string title)
        {
            var packages = await _packageRepository.GetPackageByTitleAsync
            (title);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/travelagent")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByTravelagentAsync([FromQuery] int travelagent)
        {
            var packages = await _packageRepository.GetPackageByTravelagentAsync(travelagent);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search")]
        //[Authorize]
        public async Task<IActionResult>
        GetPackageByPriceDurationTitleAsync([FromQuery] long price,[FromQuery] int duration, [FromQuery] string title)
        {
            var packages = await
            _packageRepository.GetPackageByPriceDurationTitleAsync
            (price, duration, title);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/price-duration")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByPriceDurationAsync
        ([FromQuery] long price, [FromQuery] int duration)
        {
            var packages = await
            _packageRepository.GetPackageByPriceDurationAsync(price,
            duration);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/duration")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByDurationAsync
        ([FromQuery] int duration)
        {
            var packages = await
            _packageRepository.GetPackageByDurationAsync(duration);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }

        [HttpGet("search/price-range")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByPriceRangeAsync([FromQuery] long minPrice, [FromQuery] long maxPrice)
        {
            var packages = await
            _packageRepository.GetPackageByPriceRangeAsync(minPrice,
            maxPrice);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/includedservices")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByIncludedServicesAsync
        ([FromQuery] string includedservices)
        {
            var packages = await
            _packageRepository.GetPackageByIncludedServicesAsync
            (includedservices);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/price")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByPriceAsync([FromQuery] long price)
        {
            var packages = await _packageRepository.GetPackageByPriceAsync
            (price);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }
        [HttpGet("search/Category")]
        //[Authorize]
        public async Task<IActionResult> GetPackageByCategoryAsync([FromQuery] string category)
        {
            var packages = await
            _packageRepository.GetPackageByCategoryAsync(category);
            if (packages == null || !packages.Any())
            {
                return NotFound();
            }
            return Ok(packages);
        }



        [HttpPost]
        //[Authorize(Roles = "Admin,Travel Agent")]
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
                Category = newPackage.Category,
                Travelagent = newPackage.Travelagent,
                image=newPackage.image
            };

            await _packageRepository.AddPackagesAsync(package);
            return Ok(package);
        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "Travel Agent, Admin")]

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
                Category = updatedPackage.Category,
                Travelagent = updatedPackage.Travelagent,
                image=updatedPackage.image

            };

            await _packageRepository.UpdatePackageAsync(package.PackageID, package.Title, package.Description, package.Duration, package.Price, package.IncludedServices,package.Travelagent,package.image);
            return Ok (package);
        }
        
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageRepository.DeletePackageAsync(id);
            return NoContent();
        }
        // Method to fetch packages based on the number of bookings
        [HttpGet("popular")]
        //[Authorize]
        public async Task<IActionResult> GetPackagesByBookings()
        {
            var packages = await _packageRepository.GetPackagesByBookingsAsync();
            return Ok(packages.OrderByDescending(p => p.Bookings.Count)); // Assumes Bookings is populated
        }

        // Method to fetch packages based on average rating
        [HttpGet("rating")]
        //[Authorize]
        public async Task<IActionResult> GetPackagesByRating()
        {
            var packages = await
            _packageRepository.GetPackagesByRatingAsync();
            return Ok(packages);
        }



        // Method to fetch packages based on recent reviews
        [HttpGet("reviews/recent")]
        //[Authorize]
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
        //[Authorize]
        public async Task<IActionResult> GetPackagesByReviewCount()
        {
            var packages = await _packageRepository.GetPackagesByReviewCountAsync();
            return Ok(packages);
        }

    }
}