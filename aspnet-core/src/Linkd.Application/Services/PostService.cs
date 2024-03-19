using AutoMapper;
using Linkd.Dtos;
using Linkd.Interfaces;
using Linkd.IRepository;
using Linkd.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Linkd.Services
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Post, Guid> _iPostRepository;
        private readonly ICurrentUser _currentUser;
        public PostService(IPostRepository postRepository, IMapper mapper,
            IRepository<Post, Guid> iPostRepository,ICurrentUser currentUser)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _iPostRepository = iPostRepository;
            _currentUser = currentUser;
        }

        #region methods

        public async Task<ResponseDto> GetPostsAsync()
        {
            var response = new ResponseDto();
            try
            {
                var data = await _postRepository.GetPostsAsync();
                var mappedData = _mapper.Map<List<Post>>(data);

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

        public async Task<ResponseDto> GetPostByIdAsync(Guid id)
        {
            var response = new ResponseDto();
            try
            {
                var posts = await _postRepository.GetPostByIdAsync(id);
                if(posts == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Posts not found for the specified post Id";
                    return response;
                }
                var data = await _postRepository.GetPostByIdAsync(posts.Id);
                var postsdata = _mapper.Map<Post>(data);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = postsdata;
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;

            }
            return response;
        }
        public async Task<ResponseDto> GetPostByUserIdAsync()
        {
            var response = new ResponseDto();
            try
            {
                var userid = _currentUser.Id;
                var posts = await _postRepository.GetPostsByUserId((Guid)userid);
                if (posts == null || !posts.Any()) 
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Posts not found for the specified user Id";
                    return response;
                }
                var postsdata = _mapper.Map<List<Post>>(posts);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = postsdata;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> GetPostsOfConnectedUsers()
        {
            var response = new ResponseDto();
            try
            {
                var userid = _currentUser.Id;
                var posts = await _postRepository.GetPostsOfConnectedUsersAsync((Guid)userid);
                if (posts == null || !posts.Any())
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Posts not found for the specified user Id";
                    return response;
                }
                var postsdata = _mapper.Map<List<Post>>(posts);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = postsdata;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> AddPostAsync(AddPostDto post)
        
        {
            var response = new ResponseDto();
            try
            {
                if (post.ImageUrl != null)
                {
                    // Check image type
                    if (post.ImageUrl.ContentType == null ||
                        (!post.ImageUrl.ContentType.Contains("jpeg") && !post.ImageUrl.ContentType.Contains("png")))
                    {
                        response.Status = 400;
                        response.Message = "Invalid file format, only jpeg and png images are allowed";
                        response.Error = "Invalid type";
                        return response;
                    }

                    FileInfo images = new FileInfo(post.ImageUrl.FileName);
                    var uploadImage = Path.Combine("wwwroot", "images");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(images.Name);
                    string filePath = Path.Combine(uploadImage, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        post.ImageUrl.CopyTo(fileStream);
                    }
                    string relativeFilePath = filePath.Substring(filePath.IndexOf("images"));

                    // image to convert base64
                    byte[] imgData;
                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (var ms = new MemoryStream())
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bytesRead);
                            }
                            imgData = ms.ToArray();
                        }
                    }
                    string baseImage = Convert.ToBase64String(imgData);
                    var userid = _currentUser.Id;

                    var postData = new Post
                    {
                        Content = post.Content,
                        ImageUrl = "data:image/jpeg;base64," + baseImage,
                        CreationTime = DateTime.Now,
                        UserId = (Guid)userid
                    };
                    var pData = _mapper.Map<Post>(postData);
                    //await _postRepository.AddPostAsync(pData);
                    var abc = await _iPostRepository.InsertAsync(pData);
                    response.Status = 200;
                    response.Message = "Post added successfully";
                    response.Data = postData;
                }
                else
                {
                    var userid = _currentUser.Id;
                    var postData = new Post
                    {
                        Content = post.Content,
                        CreationTime = DateTime.Now,
                        UserId = (Guid)userid
                    };
                    var pData = _mapper.Map<Post>(postData);
                    //await _postRepository.AddPostAsync(pData);
                    var abc = await _iPostRepository.InsertAsync(pData);
                    response.Status = 200;
                    response.Message = "Post added successfully";
                    response.Data = postData;
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public async Task<ResponseDto> UpdatePostAsync(UpdatePostDto post)
        {
            var response = new ResponseDto();
            try
            {
                var existingPost = await _postRepository.GetPostByIdAsync(post.Id);
                if (existingPost == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Post not found";
                    return response;
                }

                existingPost.Content = post.Content;

                await _iPostRepository.UpdateAsync(existingPost);

                response.Status = 200;
                response.Message = "Updated";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
    }
        public async Task<ResponseDto> DeletePostAsync(Guid id)
        {
            var response = new ResponseDto();
            try
            {
                var databyId = await _postRepository.GetPostByIdAsync(id);
                if (databyId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Post not found";
                    return response;
                }

                await _iPostRepository.DeleteAsync(id); 

                response.Status = 204;
                response.Message = "Deleted";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetUsersAsync()
        {
            var response = new ResponseDto();
            try
            {
                var userId = _currentUser.Id;
                var data = await _postRepository.GetUsersAsync((Guid)userId);
                var mappedData = _mapper.Map<List<IdentityUser>>(data);

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
        #endregion
    }
}
