using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Data;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services
{
    public class EssayImageRepository:RepositoryBase<EssayImage, Guid>, IEssayImageRepository
    {
        private readonly BlogDbContext _dbContext;

        public EssayImageRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EssayImage> GetByIdAsync(Guid userId, Guid essayId, Guid essayImageId)
        {
            return await _dbContext.Set<EssayImage>()
                .Include(x => x.Essay)
                    .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == essayImageId&&x.Essay.Id==essayId&&x.Essay.User.Id==userId);
        }
    }
}