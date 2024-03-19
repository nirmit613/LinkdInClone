using System;

namespace Linkd.Dtos
{
    public class PostDto
    {
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
