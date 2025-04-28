using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class BookandPaymentRepository:IBookandPaymentRepository
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
                var bookings = await context.Bookings.ToListAsync<Booking>();
                return bookings;
            }
        }
        public async Task<List<Booking>> GetUpcomingBookings()
        {
            using (var context = new CombinedDbContext())
            {
                var bookings = await context.Bookings.Where(b => b.StartDate > DateTime.Now).ToListAsync();
                return bookings;
            }
        }
        public async Task<List<Booking>> GetBookingsByBookingIDAsync(int BookingID)
        {
            using (var context = new CombinedDbContext())
            {
                var bookings = await context.Bookings.Where(a => a.BookingID == BookingID).ToListAsync();
                return bookings;
            }
        }
        public async Task<List<Booking>> GetBookingsByUserID(int UserID)
        {
            using (var context = new CombinedDbContext())
            {
                var bookings = await context.Bookings.Where(a => a.UserID == UserID).ToListAsync();
                return bookings;
            }
        }
        public async Task UpdateBookingAsync(long BookingID, DateTime StartDate)
        {
            using (var context = new CombinedDbContext())
            {
                var user = context.Bookings.Find(BookingID);
                if (user != null)
                {
                    user.StartDate = StartDate;
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task CancelBooking(int BookingID)
        {
            using (var context = new CombinedDbContext())
            {
                var booking = await context.Bookings.FindAsync(BookingID);
                if (booking != null)
                {
                    booking.Status = "Cancelled";
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<List<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var context = new CombinedDbContext())
            {
                var bookings = await context.Bookings.Where(b => b.StartDate >= startDate && b.EndDate <= endDate).ToListAsync();
                return bookings;
            }
        }
        public async Task DeleteBookingAsync(int BookingID)
        {
            using (var dbContext = new CombinedDbContext())
            {
                var user = dbContext.Bookings.Find(BookingID);
                if (user != null)
                {
                    dbContext.Bookings.Remove(user);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
