using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Linkd.Models
{
    public class Post: FullAuditedAggregateRoot<Guid>
    {
        public string? Content {  get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreationTime { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public  IdentityUser User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
