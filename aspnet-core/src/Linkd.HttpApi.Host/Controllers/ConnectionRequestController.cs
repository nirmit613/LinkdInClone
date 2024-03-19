using Linkd.Interfaces;
using Linkd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Linkd.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Linkd.Controllers
{
    [Route("api/connectionRequest")]
    [Authorize]
    public class ConnectionRequestController : ControllerBase
    {
        private readonly IConnectionRequestService _connectionRequestService;
        public ConnectionRequestController(IConnectionRequestService connectionRequestService)
        {
            _connectionRequestService = connectionRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _connectionRequestService.GetConnectionsAsync();
            return Ok(posts);
        }
        [HttpGet("connectionId")]
        public async Task<IActionResult> GetConnectionRequestById(Guid connectionId)
        {
            var data = await _connectionRequestService.GetConnectionRequestByIdAsync(connectionId);
            return Ok(data);
        }
        [HttpGet("receiverId")]
        public async Task<IActionResult> GetConnectionByReceiverId()
        {
            var data = await _connectionRequestService.GetConnectionRequestByReceiverIdAsync();
            return Ok(data);
        }
        [HttpGet("senderId")]
        public async Task<IActionResult> GetConnectionBySenderId()
        {
            var data = await _connectionRequestService.GetConnectionRequestBySenderIdAsync();
            return Ok(data);
        }
        [HttpPost("post")]
        public async Task<IActionResult> SendRequest(Guid receiverId)
        {
            var addData = await _connectionRequestService.AddConnectionRequest(receiverId);
            return Ok(addData);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRequest([FromBody]UpdateConnectionDto connection)
        {
            var updateData = await _connectionRequestService.UpdateConnectionRequest(connection);
            return Ok(updateData);
        }

    }
}
