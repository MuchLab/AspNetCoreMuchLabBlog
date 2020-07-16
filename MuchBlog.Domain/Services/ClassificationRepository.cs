using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Data;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Domain.Services
{
    public class ClassificationRepository:RepositoryBase<Classification, Guid>, IClassificationRepository
    {
        private readonly DbContext _dbContext;

        public ClassificationRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Classification>> GetAllAsync(Guid essayId)
        {
            return await _dbContext.Set<Classification>()
                .Where(x => x.EssayClassifications.Any(x => x.EssayId == essayId))
                .ToListAsync();
        }

        public void SelectClassification(IEnumerable<EssayClassification> addList, IEnumerable<EssayClassification> deleteList)
        {
            _dbContext.Set<EssayClassification>().AddRange(addList);
            _dbContext.Set<EssayClassification>().RemoveRange(deleteList);
        }

        public async Task<IEnumerable<Classification>> FindClassificationsByName(string[] className)
        {
            return await _dbContext.Set<Classification>()
                .Where(x => className.Contains(x.Name))
                .ToListAsync();
        }

        public bool EssayClassificationExist(Guid essayId, Guid classificationId)
        {
            return _dbContext.Set<EssayClassification>()
                .Any(x => x.ClassificationId == classificationId && x.EssayId == essayId);
        }
    }
}