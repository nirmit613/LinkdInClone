using Linkd.Dtos;
using System;
using System.Threading.Tasks;

namespace Linkd.Interfaces
{
    public interface ICommentService
    {
        Task<ResponseDto> GetCommentByIdAsync(Guid commentId);
        Task<ResponseDto> GetCommentsForPostAsync(Guid postId);
        Task<ResponseDto> AddCommentAsync(AddCommentDto comment);
        //Task<ResponseDto> UpdateCommentAsync(UpdateCommentDto comment);
        //Task<ResponseDto> DeleteCommentAsync(Guid commentId);
    }
}
