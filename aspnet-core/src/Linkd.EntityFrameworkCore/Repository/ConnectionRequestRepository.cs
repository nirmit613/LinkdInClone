using Linkd.EntityFrameworkCore;
using Linkd.IRepository;
using Linkd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linkd.Repository
{
    public class ConnectionRequestRepository:IConnectionRequestRepository
    {
        private readonly LinkdDbContext _context;

        public ConnectionRequestRepository(LinkdDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConnectionRequest>> GetConnectionsAsync()
        {
            var data = await _context.ConnectionRequests.Include(u => u.Sender).ToListAsync();
            return data;
        }
        public async Task<ConnectionRequest> GetConnectionRequestByIdAsync(Guid id)
        {
            return await _context.ConnectionRequests.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<ConnectionRequest>> GetConnRequestsByReceiverId(Guid ReceiverId)
        {
            return await _context.ConnectionRequests.Where(u=>u.ReceiverId == ReceiverId).Include(u=>u.Sender).OrderByDescending(u => u.DateOfRequest).ToListAsync();
        }
        public async Task<IEnumerable<ConnectionRequest>> GetConnRequestsBySenderId(Guid senderId)
        {
            return await _context.ConnectionRequests.Where(u => u.SenderId == senderId).Include(u => u.Sender).OrderByDescending(u => u.DateOfRequest).ToListAsync();
        }
        public async Task AddConnectionRequestAsync(ConnectionRequest connectionRequest)
        {
            await _context.ConnectionRequests.AddAsync(connectionRequest);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateConnectionRequestAsync(ConnectionRequest connectionRequest)
        {
            _context.ConnectionRequests.Update(connectionRequest);
            _context.Entry(connectionRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteConnectionRequestAsync(Guid id)
        {
            var data = await _context.ConnectionRequests.FindAsync(id);
            if (data != null)
            {
                _context.ConnectionRequests.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
