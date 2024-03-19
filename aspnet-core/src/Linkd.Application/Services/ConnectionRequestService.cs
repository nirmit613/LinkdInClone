using AutoMapper;
using Linkd.Dtos;
using Linkd.Interfaces;
using Linkd.IRepository;
using Linkd.Models;
using Linkd.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Linkd.Services
{
    public class ConnectionRequestService:IConnectionRequestService
    {
        private readonly IConnectionRequestRepository _connectionRequestRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<ConnectionRequest,Guid> _iPostRepository;
        private readonly ICurrentUser _currentUser;

        public ConnectionRequestService(IConnectionRequestRepository connectionRequestRepository, IMapper mapper, IRepository<ConnectionRequest, Guid> iPostRepository, ICurrentUser currentUser)
        {
            _connectionRequestRepository = connectionRequestRepository;
            _mapper = mapper;
            _iPostRepository = iPostRepository;
            _currentUser = currentUser;
        }
        public async Task<ResponseDto> GetConnectionsAsync()
        {
            var response = new ResponseDto();
            try
            {
                var data = await _connectionRequestRepository.GetConnectionsAsync();
                var mappedData = _mapper.Map<List<ConnectionRequest>>(data);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = mappedData;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetConnectionRequestByIdAsync(Guid id)
        {
            var response = new ResponseDto();
            try
            {
                var data = await _connectionRequestRepository.GetConnectionRequestByIdAsync(id);
                if (data != null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Connection request not found";
                    return response;
                }
                var connectionData = await _connectionRequestRepository.GetConnectionRequestByIdAsync(data.Id);
                var connDatas = _mapper.Map<ConnectionRequest>(connectionData);
                response.Status = 200;
                response.Message = "Ok";
                response.Data = connDatas;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;

            }
            return response;
        }
        public async Task<ResponseDto> GetConnectionRequestByReceiverIdAsync()
        {
            var response = new ResponseDto();
            try
            {
                var receiverId = _currentUser.Id;
                var data = await _connectionRequestRepository.GetConnRequestsByReceiverId((Guid)receiverId);
                if(data == null || !data.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Requests not found for the specified receiver Id";
                    return response;
                }
                var connData = _mapper.Map<List<ConnectionRequest>>(data);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = connData;
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> GetConnectionRequestBySenderIdAsync()
        {
            var response = new ResponseDto();
            try
            {
                var senderId = _currentUser.Id;
                var data = await _connectionRequestRepository.GetConnRequestsBySenderId((Guid)senderId);
                if (data == null || !data.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Requests not found for the specified receiver Id";
                    return response;
                }
                var connData = _mapper.Map<List<ConnectionRequest>>(data);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = connData;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> AddConnectionRequest(Guid connection)
        {
            var response = new ResponseDto();
            try
            {
                var senderId = _currentUser.Id;

                var existingConnection = await _iPostRepository.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == connection);

                if (existingConnection != null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Connection request already sent to this user.";
                    return response;
                }

                var connectionData = new ConnectionRequest
                {
                    SenderId = (Guid)senderId,
                    ReceiverId = connection,
                    DateOfRequest = DateTime.UtcNow,
                    RequestStatus = "Pending",
                };
                var connData = _mapper.Map<ConnectionRequest>(connectionData);
                var addConn = await _iPostRepository.InsertAsync(connData);
                response.Status = 200;
                response.Message = "Request added successfully";
                response.Data = connectionData;

            }
            catch(Exception ex )
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;

            }
            return response;
        }
        public async Task<ResponseDto> UpdateConnectionRequest(UpdateConnectionDto connection)
        {
            var response = new ResponseDto();
            try
            {
                var existingRequest = await _connectionRequestRepository.GetConnectionRequestByIdAsync(connection.Id);
                if(existingRequest == null)
                {
                    response.Status = 404;
                    response.Message = "Connection request not found";
                    return response;
                }
                if(connection.RequestStatus.ToLower() == "accepted")
                {
                    existingRequest.RequestStatus = "Accepted";
                }
                else if(connection.RequestStatus.ToLower() == "rejected")
                {
                    existingRequest.RequestStatus = "Rejected";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Invalid request status";
                    return response;
                }
                existingRequest.ReceiverId = (Guid)_currentUser.Id;
                await _iPostRepository.UpdateAsync(existingRequest);
                response.Status = 200;
                response.Message = "Connection request updated successfully";
                response.Data = _mapper.Map<UpdateConnectionDto>(existingRequest);

            }
            catch( Exception ex )
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
    }
}
