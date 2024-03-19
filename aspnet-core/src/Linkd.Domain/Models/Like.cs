using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Linkd.Models
{
    public class Like:FullAuditedAggregateRoot<Guid>
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey("Post")]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
