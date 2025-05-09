using System.Collections.Generic;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetReviews()
        {
            return Ok(await _reviewRepository.GetAllReviewsAsync());
        }

        [HttpGet("{id}")]
        //[Authorize]

        public async Task<ActionResult> GetReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO newReview)
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
                Rating = newReview.Rating,
                TimeStamp = newReview.TimeStamp,
                FoodReview= newReview.FoodReview,
                FlightReview = newReview.FlightReview,
                HotelReview = newReview.HotelReview,
                TravelAgentReview = newReview.TravelAgentReview
            };

            await _reviewRepository.AddReviewAsync(review);
            return Ok(review);
        }


        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutReview(int id, ReviewDTO review)
        {
            if (id != review.ReviewID)
            {
                return BadRequest();
            }

            // Explicitly specify the method to resolve ambiguity
            await _reviewRepository.UpdateReviewAsync(
                reviewID: review.ReviewID,
                rating: review.Rating,
                comment: review.Comment,
                timeStamp: review.TimeStamp

            );
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
            return NoContent();
        }



        [HttpGet("count")]
       // [Authorize]
        public async Task<ActionResult<int>> GetReviewCount()
        {
            return Ok(await _reviewRepository.ReviewCountAsync());
        }

        [HttpGet("package/{packageID}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByPackageID(int packageID)
        {
            return Ok(await _reviewRepository.FetchReviewsByPackageIDAsync(packageID));
        }

        [HttpGet("user/{userID}")]
        //[Authorize(Roles = "Travel Agent, Admin")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByUser(int userID)
        {
            return Ok(await _reviewRepository.FetchReviewsByUserAsync(userID));
        }

        [HttpGet("rating/{rating}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByRating(int rating)
        {
            return Ok(await _reviewRepository.FetchReviewsByRatingAsync(rating));
        }

        [HttpGet("recent/{count}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> GetRecentReviews(int count)
        {
            return Ok(await _reviewRepository.FetchRecentReviewsAsync(count));
        }

        [HttpGet("top-rated/{count}")]
      //  [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> GetTopRatedReviews(int count)
        {
            return Ok(await _reviewRepository.FetchTopRatedReviewsAsync(count));
        }

        [HttpGet("average-rating/{packageID}")]
       // [Authorize]
        public async Task<ActionResult<double>> GetAverageRating(int packageID)
        {
            return Ok(await _reviewRepository.FetchAverageRatingAsync(packageID));
        }

        [HttpGet("keyword/{keyword}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByKeyword(string keyword)
        {
            return Ok(await _reviewRepository.FetchReviewsByKeywordAsync(keyword));
        }

       [HttpGet("top-rated-food")]
        public async Task<ActionResult<List<Review>>> GetTopRatedFoodReviews(int count)
        {
            var reviews = await _reviewRepository.FetchTopRatedFoodReviewsAsync(count);
            return Ok(reviews);
        }

        [HttpGet("top-rated-hotel")]
        public async Task<ActionResult<List<Review>>> GetTopRatedHotelReviews(int count)
        {
            var reviews = await _reviewRepository.FetchTopRatedHotelReviewsAsync(count);
            return Ok(reviews);
        }

        [HttpGet("top-rated-flight")]
        public async Task<ActionResult<List<Review>>> GetTopRatedFlightReviews(int count)
        {
            var reviews = await _reviewRepository.FetchTopRatedFlightReviewsAsync(count);
            return Ok(reviews);
        }

        [HttpGet("top-rated-travel-agent")]
        public async Task<ActionResult<List<Review>>> GetTopRatedTravelAgentReviews(int count)
        {
            var reviews = await _reviewRepository.FetchTopRatedTravelAgentReviewsAsync(count);
            return Ok(reviews);
        }

    }
}
