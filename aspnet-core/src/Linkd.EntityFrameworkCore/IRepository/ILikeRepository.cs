using Linkd.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Linkd.IRepository
{
    public interface ILikeRepository
    {
        Task<bool> IsPostLikedByUserAsync(Guid userId, Guid postId);
        Task<int> GetTotalLikesForPostAsync(Guid postId);
    }
}
