using Linkd.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Linkd.IRepository
{
    public interface IConnectionRequestRepository
    {
        Task<IEnumerable<ConnectionRequest>> GetConnectionsAsync();
        Task<ConnectionRequest> GetConnectionRequestByIdAsync(Guid id);
        Task<IEnumerable<ConnectionRequest>> GetConnRequestsByReceiverId(Guid receiverId);
        Task<IEnumerable<ConnectionRequest>> GetConnRequestsBySenderId(Guid senderId);
        Task AddConnectionRequestAsync(ConnectionRequest connectionRequest);
        Task UpdateConnectionRequestAsync(ConnectionRequest connectionRequest);
        Task DeleteConnectionRequestAsync(Guid id);
    }
}
