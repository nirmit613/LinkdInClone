using System;
using Volo.Abp.Application.Dtos;

namespace Linkd.Dtos
{
    public class UpdatePostDto:EntityDto<Guid>
    {
        public string? Content { get; set; }
        public Guid UserId { get; set; }
    }
}
