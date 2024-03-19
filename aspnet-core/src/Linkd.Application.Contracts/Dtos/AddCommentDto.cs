using System;

namespace Linkd.Dtos
{
    public class AddCommentDto
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
