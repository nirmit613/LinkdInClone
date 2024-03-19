using Linkd.Dtos;
using Linkd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Linkd.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("postId")]
        public async Task<IActionResult> GetCommentsForPost(Guid postId)
        {
            var data = await _commentService.GetCommentsForPostAsync(postId);
            return Ok(data);
        }
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment(AddCommentDto comment)
        {
            var data = await _commentService.AddCommentAsync(comment);
            return Ok(data);
        }

    }
}
