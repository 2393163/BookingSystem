using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        
        
        public async Task<User> ValidateUser(string email, string password)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
        }

        public async Task AddUsers(User newuser)
        {
            using (var context = new CombinedDbContext())
            {
                await context.Users.AddAsync(newuser);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.ToListAsync<User>();
            }
        }

        public async Task<List<User>> GetUsersByName(string UserName)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.Where(a => a.Name.Contains(UserName)).ToListAsync<User>();
            }
        }

        public async Task UpdateUser(long UserId, string newName, string newEmail, string newContact)
        {
            using (var dbContext = new CombinedDbContext())
            {
                var user = await dbContext.Users.FindAsync(UserId);
                if (user != null)
                {
                    user.Name = newName;
                    user.Email = newEmail;
                    user.ContactNumber = newContact;
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteUser(long userId)
        {
            using (var dbContext = new CombinedDbContext())
            {
                var user = await dbContext.Users.FindAsync(userId);
                if (user != null)
                {
                    dbContext.Users.Remove(user);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<User> GetUserById(long id)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            }
        }

        public async Task<int> GetTotalUsers()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.CountAsync();
            }
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Users.Where(u => u.Role == role).ToListAsync();
            }
        }
    }
}
