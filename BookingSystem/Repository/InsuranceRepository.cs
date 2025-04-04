using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class InsuranceRepository
    {
        public async Task AddInsuranceAsync(Insurance newInsurance)
        {
            using (var context = new CombinedDbContext())
            {
                await context.Insurances.AddAsync(newInsurance);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Insurance>> GetAllInsurancesAsync()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Insurances.ToListAsync();
            }
        }

        public async Task UpdateInsuranceAsync(int InsuranceID, string CoverageDetails)
        {
            using (var context = new CombinedDbContext())
            {
                var insurance = await context.Insurances.FindAsync(InsuranceID);
                if (insurance != null)
                {
                    insurance.CoverageDetails = CoverageDetails;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteInsuranceAsync(int InsuranceID)
        {
            using (var context = new CombinedDbContext())
            {
                var insurance = await context.Insurances.FindAsync(InsuranceID);
                if (insurance != null)
                {
                    context.Insurances.Remove(insurance);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
