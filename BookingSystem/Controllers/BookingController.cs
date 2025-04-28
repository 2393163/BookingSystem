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
    public class BookingController : ControllerBase
    {
        private readonly IBookandPaymentRepository _bookandPaymentRepository;

        public BookingController(IBookandPaymentRepository bookandPaymentRepository)
        {
            _bookandPaymentRepository = bookandPaymentRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookandPaymentRepository.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = (await _bookandPaymentRepository.GetBookingsByBookingIDAsync(id)).FirstOrDefault();
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        [Authorize(Roles = "Travel Agent, Admin")]
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

            await _bookandPaymentRepository.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, booking);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO updatedBooking)
        {
            if (updatedBooking == null || updatedBooking.BookingID != id)
            {
                return BadRequest("Booking data is invalid.");
            }

            await _bookandPaymentRepository.UpdateBookingAsync(id, updatedBooking.StartDate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Travel Agent, Admin")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookandPaymentRepository.DeleteBookingAsync(id);
            return NoContent();
        }
    }
}
