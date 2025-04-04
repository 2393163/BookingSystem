using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public class PackageRepository
    {
        private readonly CombinedDbContext _context;

        public PackageRepository(CombinedDbContext context)
        {
            _context = context;
        }

        public async Task AddPackagesAsync(Package newpackage)
        {
            await _context.Packages.AddAsync(newpackage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Package>> GetAllPackagesAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<List<Package>> GetPackageByTitleAsync(string title)
        {
            return await _context.Packages.Where(a => a.Title == title).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByPackageIdAsync(int packageid)
        {
            return await _context.Packages.Where(a => a.PackageID == packageid).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByPriceRangeAsync(long minPrice, long maxPrice)
        {
            return await _context.Packages.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByDurationAsync(int duration)
        {
            return await _context.Packages.Where(a => a.Duration == duration).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByPriceAsync(long price)
        {
            return await _context.Packages.Where(a => a.Price == price).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByCategoryAsync(string category)
        {
            return await _context.Packages.Where(a => a.Category == category).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByPriceDurationTitleAsync(long price, int duration, string title)
        {
            return await _context.Packages.Where(p => p.Price <= price && p.Duration == duration && p.Title.Contains(title)).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByPriceDurationAsync(long price, int duration)
        {
            return await _context.Packages.Where(p => p.Price == price && p.Duration == duration).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByIncludedServicesAsync(string includedservices)
        {
            return await _context.Packages.Where(p => p.IncludedServices.Contains(includedservices)).ToListAsync();
        }

        public async Task<List<Package>> GetPackageByDescriptionAsync(string description)
        {
            return await _context.Packages.Where(p => p.Description.Contains(description)).ToListAsync();
        }

        public async Task UpdatePackageAsync(int PackageID, string Title, string Description, int Duration, long Price, string IncludedServices)
        {
            var package = await _context.Packages.FindAsync(PackageID);
            if (package != null)
            {
                package.Title = Title;
                package.Duration = Duration;
                package.Description = Description;
                package.Price = Price;
                package.IncludedServices = IncludedServices;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePackageAsync(int PackageId)
        {
            var package = await _context.Packages.FindAsync(PackageId);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }
}
