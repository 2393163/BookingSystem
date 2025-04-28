using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public interface IInsuranceRepository
    {

        Task <Insurance> AddInsuranceAsync(long userId, long bookingId);
        Task<List<Insurance>> GetAllInsurancesAsync();
        Task <Insurance> UpdateInsuranceStatusAsync(long InsuranceID, string status);
        Task DeleteInsuranceAsync(int InsuranceID);
        Task<IEnumerable<Insurance>> GetInsurancesByUserIdAsync(long userId);
        Task<List<Insurance>> GetInsuranceByProviderAsync(string provider);
        Task<int> GetTotalInsuranceCountAsync();

        




    }
}
