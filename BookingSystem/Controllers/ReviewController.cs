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
    public class ReviewController : ControllerBase
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewController(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return Ok(await _reviewRepository.GetAllReviewsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(string email,[FromBody] ReviewDTO newReview)
        {
            if (newReview == null)
            {
                return BadRequest("Review is null.");
            }

            var review = new Review
            {
                ReviewID = newReview.ReviewID,
                UserID = newReview.UserID,
                PackageID = newReview.PackageID,
                Comment = newReview.Comment,
                Rating = newReview.Rating
            };

            await _reviewRepository.AddReviewAsync(email,review);
            return Ok(review);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewID)
            {
                return BadRequest();
            }

            await _reviewRepository.UpdateReviewAsync(id, review.Rating, review.Comment, review.TimeStamp);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
