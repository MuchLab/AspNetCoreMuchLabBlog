using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Domain.Helpers;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Data;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services
{
    /// <summary>
    /// 使用RepositoryBase来实现仓储的公共部分，
    /// IEssayRepository来扩展逻辑，和使用依赖注入框架 
    /// </summary>
    public class EssayRepository:RepositoryBase<Essay, Guid>, IEssayRepository
    {
        private readonly DbContext _dbContext;

        public EssayRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Essay>> GetEssaysAsync(Guid userId)
        {
            return await _dbContext.Set<Essay>()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Essay> GetByIdAsync(Guid userId, Guid essayId)
        {
            return await _dbContext.Set<Essay>()
                .Include(x=>x.User)
                .Include(x=>x.EssayClassifications)
                .ThenInclude(x=>x.Classification)
                .SingleOrDefaultAsync(x => x.Id == essayId && x.UserId == userId);
        }

        public Task<PageList<Essay>> GetAll(EssayResourceParameters parameters)
        {
            IQueryable<Essay> queryableEssays = _dbContext
                .Set<Essay>()
                .Include(x=>x.User)
                .Include(x=>x.EssayClassifications)
                    .ThenInclude(x=>x.Classification);
            return PageList<Essay>
                .CreateAsync(queryableEssays,
                    parameters.PageNumber,
                    parameters.PageSize);
        }


        public Task<PageList<Essay>> GetEssays(Guid userId, EssayResourceParameters parameters)
        {
            IQueryable<Essay> queryableEssays = _dbContext.Set<Essay>()
                .Where(x=>x.UserId==userId)
                .Include(x => x.User)
                .Include(x=>x.EssayClassifications)
                    .ThenInclude(x=>x.Classification);
            return PageList<Essay>
                .CreateAsync(queryableEssays,
                    parameters.PageNumber,
                    parameters.PageSize);
        }

        public int EssaysCount()
        {
            return _dbContext.Set<Essay>().Count();
        }
        public int EssaysCount(Guid userId)
        {
            return _dbContext
                .Set<Essay>()
                .Count(x => x.UserId==userId);
        }

        public int EssaysCountByClassification(Guid classificationId)
        {
            return _dbContext.Set<Essay>()
                .Include(x=>x.EssayClassifications)
                .Count(x=>x.EssayClassifications.Any(y=>y.ClassificationId==classificationId));
        }

        public async Task<IEnumerable<Essay>> GetEssaysByClassificationId(Guid classificationId)
        {
            return await _dbContext.Set<Essay>()
                .Where(x => x.EssayClassifications.Any(x => x.ClassificationId == classificationId))
                .Include(x=>x.User)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Essay>> GetAllAsync()
        {
             return await _dbContext.Set<Essay>()
                    .Include(x => x.User)
                    .ToListAsync();
        }
    }
}