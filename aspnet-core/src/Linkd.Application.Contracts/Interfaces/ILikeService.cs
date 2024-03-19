using Linkd.Dtos;
using System;
using System.Threading.Tasks;

namespace Linkd.Interfaces
{
    public interface ILikeService
    {
        Task<ResponseDto> LikePostAsync(AddLikeDto like);
        Task<ResponseDto> UnlikePostAsync(AddLikeDto like);
        Task<ResponseDto> GetTotalLikesForPostAsync(Guid postId);
        Task<bool> IsPostLikedByUserAsync(Guid postId);
    }
}
