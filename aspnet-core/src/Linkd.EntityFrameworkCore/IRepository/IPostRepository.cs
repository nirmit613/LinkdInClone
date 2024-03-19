using Linkd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Linkd.IRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<IEnumerable<IdentityUser>> GetUsersAsync(Guid userId);
        Task<Post> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsByUserId(Guid userId);
        Task<IEnumerable<Post>> GetPostsOfConnectedUsersAsync(Guid userId);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Guid id);
    }
}
