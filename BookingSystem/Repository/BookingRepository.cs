using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class BookingRepository: IBookingRepository
    {
        public async Task AddBookingAsync(Booking booking)
        {
            using (var context = new CombinedDbContext())
            {
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
            }
        }
        public async Task <List<Booking>> GetAllBookingsAsync()
        {
            using (var context = new CombinedDbContext())
            {
                var bookings = await context.Bookings.ToListAsync<Booking>();
                return bookings;
            }
        }
        public async Task <List<Booking>> GetUpcomingBookings()
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
        public async Task UpdateBookingAsync(long BookingID, DateTime StartDate, DateTime EndDate)
        {
            using (var context = new CombinedDbContext())
            {
                var booking = context.Bookings.Find(BookingID);
                if (booking != null)
                {
                    booking.StartDate = StartDate;
                    booking.EndDate = EndDate;
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task CancelBooking(long BookingID)
        {
            using (var context = new CombinedDbContext())
            {
                var booking = await context.Bookings.FindAsync(BookingID);
                if (booking != null)
                {
                    // Check if the current date is within 7 days after the StartDate
                    if (DateTime.Now <= booking.StartDate && DateTime.Now <= booking.StartDate.AddDays(-7))
                    {
                        booking.Status = "Cancelled";
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new InvalidOperationException("Booking can only be canceled  7 days Before the start date.");
                    }
                }
                else
                {
                    throw new KeyNotFoundException("Booking not found.");
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
        public async Task <bool> DeleteBookingAsync(long bookingID)
        {
            using (var dbContext = new CombinedDbContext())
            {
                var booking = dbContext.Bookings.Find(bookingID);
                if (booking == null)
                    return false;

                dbContext.Bookings.Remove(booking);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
