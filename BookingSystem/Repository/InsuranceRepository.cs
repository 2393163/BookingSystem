using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        public async Task<Insurance> AddInsuranceAsync(long userId, long bookingId)
        {
            using (var context = new CombinedDbContext())
            {
                // Retrieve the booking details based on the bookingId
                var booking = await context.Bookings.FirstOrDefaultAsync(b => b.BookingID == bookingId);

                if (booking == null)
                {
                    throw new Exception("Booking not found.");
                }

                // Calculate the duration between StartDate and EndDate
                var duration = (booking.EndDate - booking.StartDate).Days;

                // Initialize CoverageDetails and Provider based on the duration
                string coverageDetails = string.Empty;
                string provider = string.Empty;

                if (duration == 3)
                {
                    coverageDetails = "Quarter Refund";
                    provider = "A";
                }
                else if (duration == 5)
                {
                    coverageDetails = "Half Refund";
                    provider = "B";
                }
                else if (duration == 10)
                {
                    coverageDetails = "Full Refund";
                    provider = "C";
                }
                else
                {
                    throw new Exception("No insurance coverage available for the given duration.");
                }

                // Create the insurance entity
                var insurance = new Insurance
                {
                    UserID = userId,
                    BookingID = bookingId,
                    CoverageDetails = coverageDetails,
                    Provider = provider,
                    Status = "Active"
                };

                // Add and save the insurance entity to the database
                await context.Insurances.AddAsync(insurance);
                await context.SaveChangesAsync();

                return insurance;
            }
        }

        public async Task<List<Insurance>> GetAllInsurancesAsync()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Insurances.ToListAsync();
            }
        }

        public async Task<Insurance?> UpdateInsuranceStatusAsync(long insuranceId, string status)
        {
            using (var context = new CombinedDbContext())
            {
                var insurance = await context.Insurances.FindAsync(insuranceId);
                if (insurance != null)
                {
                    insurance.Status = status;
                    await context.SaveChangesAsync();
                }
                return insurance;
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
        public async Task<IEnumerable<Insurance>> GetInsurancesByUserIdAsync(long userId)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Insurances
                .Where(i => i.UserID == userId)
                .ToListAsync();
            }
        }
        public async Task<List<Insurance>> GetInsuranceByProviderAsync(string provider)

        {

            using (var context = new CombinedDbContext())

            {

                return await context.Insurances

                    .Where(i => i.Provider.Contains(provider))

                    .ToListAsync();

            }

        }

        public async Task<int> GetTotalInsuranceCountAsync()

        {

            using (var context = new CombinedDbContext())

            {

                return await context.Insurances.CountAsync();

            }

        }



    }
}
