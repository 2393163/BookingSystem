using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public class ReviewRepository
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

        private async Task<int?> GetUserIdByEmailAsync(string email)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("SELECT UserID FROM Users WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("@Email", email);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                return result != null ? (int?)result : null;
            }
        }

        public async Task<int> AddReviewAsync(string email, Review review)
        {
            var userId = await GetUserIdByEmailAsync(email);
            if (userId == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            using (var connection = GetConnection())
            {
                var command = new SqlCommand("INSERT INTO Review (UserID, PackageID, Rating, Comment, TimeStamp) VALUES (@UserID, @PackageID, @Rating, @Comment, @TimeStamp)", connection);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@PackageID", review.PackageID);
                command.Parameters.AddWithValue("@Rating", review.Rating);
                command.Parameters.AddWithValue("@Comment", review.Comment);
                command.Parameters.AddWithValue("@TimeStamp", review.TimeStamp);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateReviewAsync(int reviewID, int rating, string comment, DateTime timeStamp)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("UPDATE Review SET Rating = @Rating, Comment = @Comment, TimeStamp = @TimeStamp WHERE ReviewID = @ReviewID", connection);
                command.Parameters.AddWithValue("@ReviewID", reviewID);
                command.Parameters.AddWithValue("@Rating", rating);
                command.Parameters.AddWithValue("@Comment", comment);
                command.Parameters.AddWithValue("@TimeStamp", timeStamp);

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

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
                            UserID = (int)reader["UserID"],
                            PackageID = (int)reader["PackageID"],
                            Rating = (int)reader["Rating"],
                            Comment = (string)reader["Comment"],
                            TimeStamp = (DateTime)reader["TimeStamp"]
                        };
                    }
                }
            }
            return null;
        }

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
                            UserID = (int)reader["UserID"],
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
    }
}
