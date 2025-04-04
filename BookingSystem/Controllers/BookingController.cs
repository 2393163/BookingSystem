using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Entities;
using BookingSystem.Repository;
using BookingSystem.DTOs;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookandPaymentRepository _bookandPaymentRepository;

        public BookingController(BookandPaymentRepository bookandPaymentRepository)
        {
            _bookandPaymentRepository = bookandPaymentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return Ok(await _bookandPaymentRepository.GetAllBookingsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _bookandPaymentRepository.GetBookingsByBookingIDAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
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
            return Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO updatedBooking)
        {
            if (updatedBooking == null || updatedBooking.BookingID != id)
            {
                return BadRequest("Booking data is invalid.");
            }

            var booking = new Booking
            {
                StartDate = updatedBooking.StartDate,
                            
            };

            await _bookandPaymentRepository.UpdateBookingAsync(booking.BookingID,booking.StartDate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookandPaymentRepository.DeleteBookingAsync(id);
            return NoContent();
        }
    }
}
