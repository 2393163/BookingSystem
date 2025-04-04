using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repository
{
    public class AssistanceRepository
    {
        public async Task AddAssistanceRequestAsync(Assistance newAssistanceRequest)
        {
            using (var context = new CombinedDbContext())
            {
                await context.Assistances.AddAsync(newAssistanceRequest);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Assistance>> GetAllAssistanceRequestsAsync()
        {
            using (var context = new CombinedDbContext())
            {
                return await context.Assistances.ToListAsync();
            }
        }

        public async Task UpdateAssistanceRequestAsync(int RequestID, string IssueDescription)
        {
            using (var context = new CombinedDbContext())
            {
                var assistanceRequest = await context.Assistances.FindAsync(RequestID);
                if (assistanceRequest != null)
                {
                    assistanceRequest.IssueDescription = IssueDescription;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAssistanceRequestAsync(int RequestID)
        {
            using (var context = new CombinedDbContext())
            {
                var assistanceRequest = await context.Assistances.FindAsync(RequestID);
                if (assistanceRequest != null)
                {
                    context.Assistances.Remove(assistanceRequest);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
