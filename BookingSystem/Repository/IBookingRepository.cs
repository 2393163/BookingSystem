using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByBookingIDAsync(int BookingID);
        Task<List<Booking>> GetBookingsByUserID(int UserID);
        Task<List<Booking>> GetUpcomingBookings();
        Task<List<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate);

        Task UpdateBookingAsync(long BookingID, DateTime StartDate, DateTime endDate);

        Task<bool> DeleteBookingAsync(long BookingID);
        Task CancelBooking(long BookingID);



    }
}
