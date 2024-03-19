using Linkd.Dtos;
using Linkd.Interfaces;
using Linkd.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Linkd.Controllers
{
    [Route("api/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService; 
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        [HttpGet("{postId}/count")]
        public async Task<IActionResult> GetTotalLikesForPostAsync(Guid postId)
        {
            var count = await _likeService.GetTotalLikesForPostAsync(postId);
            return Ok(count);
        }
        [HttpPost("like")]
        public async Task<IActionResult> LikePostAsync(AddLikeDto like)
        {
            var likeData = await _likeService.LikePostAsync(like);
            return Ok(likeData);
        }

        [HttpPost("unlike")]
        public async Task<IActionResult> UnlikePostAsync(AddLikeDto like)
        {
            var unLikeData = await _likeService.UnlikePostAsync(like);
            return Ok(unLikeData);
        }
        [HttpGet("isPostLikedByUser")]
        public async Task<IActionResult> IsPostLikedByUser(Guid postId)
        {
            try
            {
                var isLikedByUser = await _likeService.IsPostLikedByUserAsync( postId);
                return Ok(isLikedByUser); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error" }); 
            }
        }
    }
}
