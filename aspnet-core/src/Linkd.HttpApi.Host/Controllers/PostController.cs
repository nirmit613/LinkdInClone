
using Linkd.Dtos;
using Linkd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Linkd.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostController : AbpController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users  = await _postService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var data = await _postService.GetPostByIdAsync(id);
            return Ok(data);
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> GetPostByUserId()
        {
            var data = await _postService.GetPostByUserIdAsync();
            return Ok(data);
        }

        [HttpGet("ConnectionUsers")]
        public async Task<IActionResult> GetPostByConnectedUserId()
        {
            var data = await _postService.GetPostsOfConnectedUsers();
            return Ok(data);
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddPost([FromForm]AddPostDto post)
        {
            var addData = await _postService.AddPostAsync(post);
            return Ok(addData);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostDto post)
        {
            var updateData = await _postService.UpdatePostAsync(post);
            return Ok(updateData);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var deleteData = await _postService.DeletePostAsync(id);
            return Ok(deleteData);
        }
    }
}
