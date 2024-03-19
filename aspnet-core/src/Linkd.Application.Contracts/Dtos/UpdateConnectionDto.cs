using System;
using System.Globalization;
using Volo.Abp.Application.Dtos;

namespace Linkd.Dtos
{
    public class UpdateConnectionDto: EntityDto<Guid>
    {
        public Guid SenderId { get; set; }
        public string RequestStatus {  get; set; }
    }
}
