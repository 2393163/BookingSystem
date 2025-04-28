using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

      

        #region Add Review
        public async Task<int> AddReviewAsync(Review review)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Review (UserID, PackageID, Rating, Comment, TimeStamp, FoodReview, FlightReview, HotelReview, TravelAgentReview) VALUES (@UserID, @PackageID, @Rating, @Comment, @TimeStamp, @FoodReview, @FlightReview, @HotelReview, @TravelAgentReview)", connection);
                command.Parameters.AddWithValue("@UserID", review.UserID);
                command.Parameters.AddWithValue("@PackageID", review.PackageID);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.Parameters.AddWithValue("@Comment", review.Comment);
                command.Parameters.AddWithValue("@TimeStamp", review.TimeStamp);
                command.Parameters.AddWithValue("@FoodReview", review.FoodReview);
                command.Parameters.AddWithValue("@FlightReview", review.FlightReview);
                command.Parameters.AddWithValue("@HotelReview", review.HotelReview);
                command.Parameters.AddWithValue("@TravelAgentReview", review.TravelAgentReview);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
        #endregion

        #region UpdateReviewAsync
        public async Task<int> UpdateReviewAsync(int reviewID, int rating, string comment, DateTime timeStamp, int foodReview, int flightReview, int hotelReview, int travelAgentReview)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("UPDATE Review SET Rating = @Rating, Comment = @Comment, TimeStamp = @TimeStamp, FoodReview = @FoodReview, FlightReview = @FlightReview, HotelReview = @HotelReview, TravelAgentReview = @TravelAgentReview WHERE ReviewID = @ReviewID", connection);

                command.Parameters.AddWithValue("@ReviewID", reviewID);
                command.Parameters.AddWithValue("@Rating", rating);
                command.Parameters.AddWithValue("@Comment", comment);
                command.Parameters.AddWithValue("@TimeStamp", timeStamp);
                command.Parameters.AddWithValue("@FoodReview", foodReview);
                command.Parameters.AddWithValue("@FlightReview", flightReview);
                command.Parameters.AddWithValue("@HotelReview", hotelReview);
                command.Parameters.AddWithValue("@TravelAgentReview", travelAgentReview);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
        #endregion

        #region DeleteReviewAsync
        public async Task<int> DeleteReviewAsync(int reviewID)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Review WHERE ReviewID = @ReviewID", connection);
                command.Parameters.AddWithValue("@ReviewID", reviewID);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
        #endregion

        #region Fetch By ReviewId

        public async Task<Review> GetReviewByIdAsync(int reviewID)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review WHERE ReviewID = @ReviewID", connection);
                command.Parameters.AddWithValue("@ReviewID", reviewID);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        };
                    }
                }
            }
            return null;
        }

        #endregion

        #region Get All Reviews

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Review Count

        public async Task<int> ReviewCountAsync()
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Review", connection);
                await connection.OpenAsync();
                return (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task<List<Review>> FetchReviewsByPackageIDAsync(int packageID)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review WHERE PackageID = @PackageID", connection);
                command.Parameters.AddWithValue("@PackageID", packageID);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                            
                        });
                    }
                }
            }
            return reviews;
        }

        #endregion

        #region Fetch Reviews By UserId
        public async Task<List<Review>> FetchReviewsByUserAsync(int userID)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review WHERE UserID = @UserID", connection);
                command.Parameters.AddWithValue("@UserID", userID);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }

        #endregion

        #region Fetch Reviews By Rating
        public async Task<List<Review>> FetchReviewsByRatingAsync(int rating)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review WHERE Rating = @Rating", connection);
                command.Parameters.AddWithValue("@Rating", rating);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch RecentReviews

        public async Task<List<Review>> FetchRecentReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review ORDER BY TimeStamp DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch TopRated Reviews

        public async Task<List<Review>> FetchTopRatedReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review ORDER BY Rating DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch Average Rating

        public async Task<double> FetchAverageRatingAsync(int packageID)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT AVG(Rating) FROM Review WHERE PackageID = @PackageID", connection);
                command.Parameters.AddWithValue("@PackageID", packageID);
                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                return result != null ? Convert.ToDouble(result) : 0.0;
            }
        }
        #endregion

        #region FetchReviewsByKeywordAsync

        public async Task<List<Review>> FetchReviewsByKeywordAsync(string keyword)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Review WHERE Comment LIKE @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        // Specific methods for fetching reviews by specific criteria

        #region Fetch TopRated FoodReviews
        public async Task<List<Review>> FetchTopRatedFoodReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review WHERE FoodReview IS NOT NULL ORDER BY FoodReview DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch TopRated FlightReviews

        public async Task<List<Review>> FetchTopRatedHotelReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review WHERE HotelReview IS NOT NULL ORDER BY HotelReview DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch TopRated FlightReviews

        public async Task<List<Review>> FetchTopRatedFlightReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review WHERE FlightReview IS NOT NULL ORDER BY FlightReview DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }
            return reviews;
        }
        #endregion

        #region Fetch TopRated TravelAgentReviews
        public async Task<List<Review>> FetchTopRatedTravelAgentReviewsAsync(int count)
        {
            var reviews = new List<Review>();
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT TOP (@Count) * FROM Review WHERE TravelAgentReview IS NOT NULL ORDER BY TravelAgentReview DESC", connection);
                command.Parameters.AddWithValue("@Count", count);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reviews.Add(new Review
                        {
                            ReviewID = (int)reader["ReviewID"],
                            UserID = (long)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"],
                            FoodReview = (int)reader["FoodReview"],
                            FlightReview = (int)reader["FlightReview"],
                            HotelReview = (int)reader["HotelReview"],
                            TravelAgentReview = (int)reader["TravelAgentReview"]
                        });
                    }
                }
            }

            return reviews;
        }
        #endregion









    }
}