using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Entities;

namespace BookingSystem.Repository
{
    public interface IAssistanceRepository
    {
        Task AddAssistanceRequestAsync(Assistance newAssistanceRequest);
        Task<List<Assistance>> GetAllAssistanceRequestsAsync();
        Task UpdateAssistanceRequestAsync(int RequestID, string IssueDescription, string status);
        Task DeleteAssistanceRequestAsync(int RequestID);
        Task<IEnumerable<Assistance>> GetRequestsByUserIdAsync(long userId);
    }
}
