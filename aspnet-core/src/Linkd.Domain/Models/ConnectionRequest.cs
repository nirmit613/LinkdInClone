using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Linkd.Models
{
    public class ConnectionRequest:FullAuditedAggregateRoot<Guid>
    {
        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
        public IdentityUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }
        public IdentityUser Receiver { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string RequestStatus {  get; set; }
    }
}
