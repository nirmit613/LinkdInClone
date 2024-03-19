using Linkd.EntityFrameworkCore;
using Linkd.IRepository;
using Linkd.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Linkd.Repository
{
    public class LikePostRepository:ILikeRepository
    {
        private readonly LinkdDbContext _context;

        public LikePostRepository(LinkdDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPostLikedByUserAsync(Guid userId,Guid postId)
        {
            return await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }
        public async Task<int> GetTotalLikesForPostAsync(Guid postId)
        {
            return await _context.Likes.CountAsync(l => l.PostId == postId);
        }
    }
}
