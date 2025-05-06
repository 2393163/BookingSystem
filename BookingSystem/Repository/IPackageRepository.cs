using System;

using System.Collections.Generic;

using System.Threading.Tasks;

using BookingSystem.Entities;

namespace BookingSystem.Repository

{

    public interface IPackageRepository

    {

        Task AddPackagesAsync(Package newPackage);

        Task<List<Package>> GetAllPackagesAsync();

        Task<List<Package>> GetPackageByTitleAsync(string title);

        Task<List<Package>> GetPackageByPackageIdAsync(int packageId);

        Task<List<Package>> GetPackageByPriceRangeAsync(long minPrice, long maxPrice);

        Task<List<Package>> GetPackageByDurationAsync(int duration);

        Task<List<Package>> GetPackageByPriceAsync(long price);

        Task<List<Package>> GetPackageByCategoryAsync(string category);

        Task<List<Package>> GetPackageByPriceDurationTitleAsync(long price, int duration, string title);

        Task<List<Package>> GetPackageByPriceDurationAsync(long price, int duration);

        Task<List<Package>> GetPackageByIncludedServicesAsync(string includedServices);

        Task<List<Package>> GetPackageByTravelagentAsync(int travelagent);

        Task UpdatePackageAsync(int packageId, string title, string description, int duration, long price, string includedServices,int Travelagent,string image);

        Task DeletePackageAsync(int packageId);

        Task<List<Package>> GetPackagesByRecentReviewsAsync();

        Task<List<Package>> GetPackagesByBookingsAsync();

        Task<List<Package>> GetPackagesByRatingAsync();

        Task<List<Package>> GetPackagesByReviewCountAsync();
       
    }

}

