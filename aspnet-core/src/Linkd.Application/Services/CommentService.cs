using AutoMapper;
using Linkd.Dtos;
using Linkd.Interfaces;
using Linkd.IRepository;
using Linkd.Models;
using Linkd.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Linkd.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Comment, Guid> _icommentRepository;
        private readonly ICurrentUser _currentUser;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IRepository<Comment, Guid> icommentRepository, ICurrentUser currentUser)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _icommentRepository = icommentRepository;
            _currentUser = currentUser;
        }

        public async Task<ResponseDto> GetCommentByIdAsync(Guid commentId)
        {
            var response = new ResponseDto();
            try
            {
                var comment = await _commentRepository.GetCommentByIdAsync(commentId);
                if (comment == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Comment not found for the specified comment Id";
                    return response;
                }
                var data = await _commentRepository.GetCommentByIdAsync(comment.Id);
                var commentdata = _mapper.Map<Comment>(data);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = commentdata;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;

            }
            return response;
        }
        public async Task<ResponseDto> GetCommentsForPostAsync(Guid postId)
        {
            var response = new ResponseDto();
            try
            {
                var comments = await _commentRepository.GetCommentsForPostAsync(postId);
                if (comments == null || !comments.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Comments not found for the specified post Id";
                    return response;
                }
                var commentsData = _mapper.Map<List<Comment>>(comments);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = commentsData;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> AddCommentAsync(AddCommentDto comment)
        {
            var response = new ResponseDto();
            try
            {
                var userId = _currentUser.Id;
                var commentData = new Comment
                {
                    UserId = (Guid)userId,
                    Content = comment.Content,
                    PostId = comment.PostId,
                    CreationTime = DateTime.Now,

                };
                var data = _mapper.Map<Comment>(commentData);
                var postData = await _icommentRepository.InsertAsync(commentData);
                response.Status = 200;
                response.Message = "Like added successfully";
                response.Data = postData;

            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
    }
   
}
