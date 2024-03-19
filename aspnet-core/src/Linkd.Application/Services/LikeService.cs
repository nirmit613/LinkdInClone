using AutoMapper;
using Linkd.Dtos;
using Linkd.Interfaces;
using Linkd.IRepository;
using Linkd.Models;
using Linkd.Repository;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Linkd.Services
{
    public class LikeService:ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Like, Guid> _iLikeRepository;
        private readonly ICurrentUser _currentUser;

        public LikeService(ILikeRepository likeRepository, IMapper mapper, IRepository<Like, Guid> iLikeRepository, ICurrentUser currentUser)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _iLikeRepository = iLikeRepository;
            _currentUser = currentUser;
        }

        public async Task<ResponseDto> LikePostAsync(AddLikeDto like)
        {
            var response = new ResponseDto();
            try
            {
                var userId = _currentUser.Id;
                var likeData = new Like
                {
                    UserId = (Guid)userId,
                    PostId = like.PostId,
                };
                var postData = _mapper.Map<Like>(likeData);
                var data = await _iLikeRepository.InsertAsync(postData);
                response.Status = 200;
                response.Message = "Like added successfully";
                response.Data = likeData;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> UnlikePostAsync(AddLikeDto like)
        {
            var response = new ResponseDto();
            try
            {
                var userId = (Guid)_currentUser.Id;
                var existingLike = await _iLikeRepository.FirstOrDefaultAsync(l =>
                    l.PostId == like.PostId && l.UserId == userId);

                if (existingLike != null)
                {
                    await _iLikeRepository.DeleteAsync(existingLike.Id);

                    response.Status = 200;
                    response.Message = "Like removed successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Post is not liked by the user";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> GetTotalLikesForPostAsync(Guid postId)
        {
            var response = new ResponseDto();
            try
            {
                var totalLikes = await _likeRepository.GetTotalLikesForPostAsync(postId);
                response.Status = 200;
                response.Message = $"Total likes for post {postId}: {totalLikes}";
                response.Data = totalLikes; 
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<bool> IsPostLikedByUserAsync(Guid postId)
        {
            var userId = (Guid)_currentUser.Id;
            if (userId == null)
            {
               
                return false;
            }

            return await _iLikeRepository.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }
    }
}
