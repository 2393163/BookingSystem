//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using BookingSystem.Entities;
//using BookingSystem.Repository;

//namespace BookingSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentController : ControllerBase
//    {
//        private readonly BookandPaymentRepository _bookandPaymentRepository;

//        public PaymentController(BookandPaymentRepository bookandPaymentRepository)
//        {
//            _bookandPaymentRepository = bookandPaymentRepository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
//        {
//            return Ok(await _bookandPaymentRepository.GetAllPaymentsAsync());
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Payment>> GetPayment(int id)
//        {
//            var payment = await _bookandPaymentRepository.GetPaymentByIdAsync(id);
//            if (payment == null)
//            {
//                return NotFound();
//            }
//            return Ok(payment);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
//        {
//            await _bookandPaymentRepository.AddPaymentAsync(payment);
//            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentID }, payment);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPayment(int id, Payment payment)
//        {
//            if (id != payment.PaymentID)
//            {
//                return BadRequest();
//            }

//            await _bookandPaymentRepository.UpdatePaymentAsync(id, payment.Amount, payment.Status, payment.PaymentMethod);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePayment(int id)
//        {
//            await _bookandPaymentRepository.DeletePaymentAsync(id);
//            return NoContent();
//        }
//    }
//}
