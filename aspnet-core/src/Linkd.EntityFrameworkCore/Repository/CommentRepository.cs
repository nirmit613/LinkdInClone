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
    public class CommentRepository : ICommentRepository
    {
        private readonly LinkdDbContext _context;
        public CommentRepository(LinkdDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<List<Comment>> GetCommentsForPostAsync(Guid postId)
        {
            return await _context.Comments.Include(u=>u.User).Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
