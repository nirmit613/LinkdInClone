using Linkd.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Linkd.IRepository
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<List<Comment>> GetCommentsForPostAsync(Guid postId);
    }
}
