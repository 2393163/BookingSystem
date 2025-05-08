using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public interface IReviewRepository
    {
        // CRUD operations for Reviews
        Task<int> AddReviewAsync(Review review);
        Task<int> UpdateReviewAsync(int reviewID, int rating, string comment, DateTime timeStamp);
        Task<int> DeleteReviewAsync(int reviewID);
        Task<Review> GetReviewByIdAsync(int reviewID);

        // Retrieval and Count
        Task<List<Review>> GetAllReviewsAsync();
        Task<int> ReviewCountAsync();
        Task<List<Review>> FetchReviewsByPackageIDAsync(int packageID);
        Task<List<Review>> FetchReviewsByUserAsync(int userID);

        // Filtering and Advanced Queries
        Task<List<Review>> FetchReviewsByRatingAsync(int rating);
        Task<List<Review>> FetchRecentReviewsAsync(int count);
        Task<List<Review>> FetchTopRatedReviewsAsync(int count);
        Task<double> FetchAverageRatingAsync(int packageID);
        Task<List<Review>> FetchReviewsByKeywordAsync(string keyword);

        // Additional methods for specific reviews
        Task<List<Review>> FetchTopRatedFoodReviewsAsync(int count);
        Task<List<Review>> FetchTopRatedFlightReviewsAsync(int count);
        Task<List<Review>> FetchTopRatedHotelReviewsAsync(int count);
        Task<List<Review>> FetchTopRatedTravelAgentReviewsAsync(int count);
    }
}
