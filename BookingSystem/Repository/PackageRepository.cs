using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.Entities;
using System.Buffers.Text;
using System.Text.RegularExpressions;


namespace BookingSystem.Repository
{
    public class PackageRepository : IPackageRepository
    {

        public async Task AddPackagesAsync(Package newpackage)
        {
            using (var _context = new CombinedDbContext())
            {
                await _context.Packages.AddAsync(newpackage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Package>> GetAllPackagesAsync()
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByTitleAsync(string title)

        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.Title == title).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByPackageIdAsync(int packageid)

        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.PackageID == packageid).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByPriceRangeAsync(long minPrice, long maxPrice)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByDurationAsync(int duration)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.Duration == duration).ToListAsync();
            }
        }
        public async Task<List<Package>> GetPackageByTravelagentAsync(int travelagent)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.Travelagent == travelagent ).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByPriceAsync(long price)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.Price == price).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByCategoryAsync(string category)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(a => a.Category == category).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByPriceDurationTitleAsync(long price, int duration, string title)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(p => p.Price <= price && p.Duration == duration && p.Title.Contains(title)).ToListAsync();
            }
        }

        public async Task<List<Package>> GetPackageByPriceDurationAsync(long price, int duration)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(p => p.Price == price && p.Duration == duration).ToListAsync();
            }
        }


        public async Task<List<Package>> GetPackageByIncludedServicesAsync(string includedservices)
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages.Where(p => p.IncludedServices.Contains(includedservices)).ToListAsync();
            }
        }

        

        public async Task UpdatePackageAsync(int PackageID, string Title, string Description, int Duration, long Price, string IncludedServices,int Travelagent,string image )
        {
            using (var _context = new CombinedDbContext())
            {
                var package = await _context.Packages.FindAsync(PackageID);
                if (package != null)
                {
                    package.Title = Title;
                    package.Duration = Duration;
                    package.Description = Description;
                    package.Price = Price;
                    package.IncludedServices = IncludedServices;
                    package.Travelagent = Travelagent;
                    package.image = image;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeletePackageAsync(int PackageId)
        {
            using (var _context = new CombinedDbContext())
            {
                var package = await _context.Packages.FindAsync(PackageId);
                if (package != null)
                {
                    _context.Packages.Remove(package);
                    await _context.SaveChangesAsync();
                }
            }
        }
        // Method to fetch packages based on the number of bookings
        public async Task<List<Package>> GetPackagesByBookingsAsync()
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages
                .Include(p => p.Bookings)
                .OrderByDescending(p => p.Bookings.Count)
                .ToListAsync();
            }
        }

        // Method to fetch packages based on average rating
        public async Task<List<Package>> GetPackagesByRatingAsync()
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages
                .Include(p => p.Reviews)
                .Select(p => new
                {
                    Package = p,
                    AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0
                })
                .OrderByDescending(p => p.AverageRating)
                .Select(p => p.Package)
                .ToListAsync();
            }
        }


        // Method to fetch packages based on the number of reviews
        public async Task<List<Package>> GetPackagesByReviewCountAsync()
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages
                .Include(p => p.Reviews)
                .OrderByDescending(p => p.Reviews.Count)
                .ToListAsync();
            }
        }

        // Method to fetch packages based on recent reviews
        public async Task<List<Package>> GetPackagesByRecentReviewsAsync()
        {
            using (var _context = new CombinedDbContext())
            {
                return await _context.Packages
                .Include(p => p.Reviews)
                .OrderByDescending(p => p.Reviews.Max(r => r.TimeStamp))
                .ToListAsync();
            }
        }


    }
}