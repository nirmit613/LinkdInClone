using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Linkd.Models
{
    public class Comment:FullAuditedAggregateRoot<Guid>
    {
        public string Content { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }
        [ForeignKey("Post")]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
