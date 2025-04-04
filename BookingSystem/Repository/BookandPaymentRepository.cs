using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class BookandPaymentRepository
    {
        public async Task AddBookingAsync(Booking booking)
        {
            using (var context = new CombinedDbContext())
            {
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Bookings.ToListAsync();
            }
        }

        public async Task<List<Booking>> GetBookingsByBookingIDAsync(int BookingID)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Bookings.Where(a => a.BookingID == BookingID).ToListAsync();
            }
        }

        public async Task UpdateBookingAsync(int BookingID, DateTime StartDate)
        {
            using (var context = new CombinedDbContext())
            {
                var booking = await context.Bookings.FindAsync(BookingID);
                if (booking != null)
                {
                    booking.StartDate = StartDate;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteBookingAsync(int BookingID)
        {
            using (var context = new CombinedDbContext())
            {
                var booking = await context.Bookings.FindAsync(BookingID);
                if (booking != null)
                {
                    context.Bookings.Remove(booking);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
