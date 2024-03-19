using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Dtos;

namespace Linkd.Dtos
{
    public class AddPostDto
    {
        public string? Content { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
