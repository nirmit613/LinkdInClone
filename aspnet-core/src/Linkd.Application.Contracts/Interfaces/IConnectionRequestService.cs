using Linkd.Dtos;
using System;
using System.Threading.Tasks;

namespace Linkd.Interfaces
{
    public interface IConnectionRequestService
    {
        Task<ResponseDto> GetConnectionsAsync();
        Task<ResponseDto> GetConnectionRequestByIdAsync(Guid id);
        Task<ResponseDto> GetConnectionRequestByReceiverIdAsync();
        Task<ResponseDto> GetConnectionRequestBySenderIdAsync();
        Task<ResponseDto> AddConnectionRequest(Guid connection);
        Task<ResponseDto> UpdateConnectionRequest(UpdateConnectionDto connection);
    }
}
