using Linkd.EntityFrameworkCore;
using Linkd.IRepository;
using Linkd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Linkd.Repository
{
    public class PostRepository : IPostRepository
    {

        private readonly LinkdDbContext _context;
        private readonly DataFilter _dataFilter;

        public PostRepository(LinkdDbContext context, DataFilter dataFilter)
        {
            _context = context;
            _dataFilter = dataFilter;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var data = await _context.Posts.Include(u=>u.User).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<IdentityUser>> GetUsersAsync(Guid userId)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            { 
                var data = await _context.Users.Where(u=>u.Id !=userId).ToListAsync();
                return data;
            }
        }
        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts.FirstOrDefaultAsync(u=>u.Id == id);
        }
        public async Task<IEnumerable<Post>> GetPostsByUserId(Guid userId)
        {
            return await _context.Posts.Include(u => u.User).Where(u=>u.UserId==userId).OrderByDescending(u => u.CreationTime).ToListAsync();
        }
       
        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePostAsync(Post post)
        {
           _context.Posts.Update(post);
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeletePostAsync(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Post>> GetPostsOfConnectedUsersAsync(Guid userId)
        {
            var connectedUserIds = await _context.ConnectionRequests
                .Where(c => c.SenderId == userId && c.RequestStatus == "Accepted")
                .Select(c => c.ReceiverId)
                .Union(_context.ConnectionRequests
                .Where(c => c.ReceiverId == userId && c.RequestStatus == "Accepted")
                .Select(c => c.SenderId))
                .ToListAsync();
            connectedUserIds.Add(userId);

            var postsOfConnectedUsers = await _context.Posts
                .Include(p => p.User)
                .Where(p => connectedUserIds.Contains(p.UserId)).OrderByDescending(u => u.CreationTime)
                .ToListAsync();

            return postsOfConnectedUsers;
        }

        
    }
}
