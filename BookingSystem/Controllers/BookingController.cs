using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookandPaymentController : ControllerBase
    {
        private readonly IBookandPaymentRepository _repository;

        public BookandPaymentController(IBookandPaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _repository.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("upcoming")]
        //[Authorize]
        public async Task<IActionResult> GetUpcomingBookings()
        {
            var bookings = await _repository.GetUpcomingBookings();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var bookings = await _repository.GetBookingsByBookingIDAsync(id);
            if (!bookings.Any())
            {
                return NotFound();
            }
            return Ok(bookings.FirstOrDefault());
        }

        [HttpGet("user/{userId}")]
        //[Authorize]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _repository.GetBookingsByUserID(userId);
            return Ok(bookings);
        }

        [HttpGet("date-range")]
        //[Authorize]
        public async Task<IActionResult> GetBookingsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var bookings = await _repository.GetBookingsByDateRange(startDate, endDate);
            return Ok(bookings);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddBooking([FromBody] BookingDTO newBooking)
        {
            if (newBooking == null)
            {
                return BadRequest("Booking is null.");
            }

            var booking = new Booking
            {
                BookingID = newBooking.BookingID,
                UserID = newBooking.UserID,
                PackageID = newBooking.PackageID,
                StartDate = newBooking.StartDate,
                EndDate = newBooking.EndDate,
                Status = newBooking.Status,
                PaymentID = newBooking.PaymentID
            };

            await _repository.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingID }, booking);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] DateTime startDate)
        {
            await _repository.UpdateBookingAsync(id, startDate);
            return NoContent();
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(long id)
        {
            await _repository.CancelBooking(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _repository.DeleteBookingAsync(id);
            return NoContent();
        }
    }
}
