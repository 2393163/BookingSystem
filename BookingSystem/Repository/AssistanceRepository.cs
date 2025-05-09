using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class AssistanceRepository : IAssistanceRepository
    {
        public async Task AddAssistanceRequestAsync(Assistance newAssistanceRequest)
        {
            using (var context = new CombinedDbContext())
            {
                await context.AssistanceRequests.AddAsync(newAssistanceRequest);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Assistance>> GetAllAssistanceRequestsAsync()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.AssistanceRequests.ToListAsync();
            }
        }

        public async Task UpdateAssistanceRequestAsync(int RequestID, string IssueDescription, string status)
        {
            using (var context = new CombinedDbContext())
            {
                var assistanceRequest = await context.AssistanceRequests.FindAsync(RequestID);
                if (assistanceRequest != null)
                {
                    assistanceRequest.IssueDescription = IssueDescription;
                    assistanceRequest.Status = status;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAssistanceRequestAsync(int RequestID)
        {
            using (var context = new CombinedDbContext())
            {
                var assistanceRequest = await context.AssistanceRequests.FindAsync(RequestID);
                if (assistanceRequest != null)
                {
                    context.AssistanceRequests.Remove(assistanceRequest);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<IEnumerable<Assistance>> GetRequestsByUserIdAsync(long userId)
        {
            using (var context = new CombinedDbContext())
            {
                return await context.AssistanceRequests
                .Where(r => r.UserID == userId)
                .ToListAsync();
            }
        }
    }
}