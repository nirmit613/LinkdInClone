using Linkd.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linkd.Interfaces
{
    public interface IPostService
    {
        Task<ResponseDto> GetPostsAsync();
        Task<ResponseDto> GetUsersAsync();
        Task<ResponseDto> GetPostByIdAsync(Guid id);
        Task<ResponseDto> GetPostByUserIdAsync();
        Task<ResponseDto> GetPostsOfConnectedUsers();
        Task<ResponseDto> AddPostAsync(AddPostDto post);
        Task<ResponseDto> UpdatePostAsync(UpdatePostDto post);
        Task<ResponseDto> DeletePostAsync(Guid id);
    }
}
