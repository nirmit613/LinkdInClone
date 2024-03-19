using AutoMapper;
using Linkd.Dtos;
using Linkd.Models;

namespace Linkd;

public class LinkdApplicationAutoMapperProfile : Profile
{
    public LinkdApplicationAutoMapperProfile()
    {
        CreateMap<Post,PostDto>().ReverseMap();
        CreateMap<Post,AddPostDto>().ReverseMap();
        CreateMap<Post,UpdatePostDto>().ReverseMap();
        CreateMap<ConnectionRequest,AddConnectionDto>().ReverseMap();
        CreateMap<ConnectionRequest,UpdateConnectionDto>().ReverseMap();
        CreateMap<Like,AddLikeDto>().ReverseMap();
        CreateMap<Comment,AddCommentDto>().ReverseMap();
    }
}
