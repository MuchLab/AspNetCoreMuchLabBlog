using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Data;

namespace MuchBlog.Domain.Services
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly IUserRepository _userRepository = null;
        private readonly IEssayRepository _essayRepository = null;
        private readonly IEssayImageRepository _essayImageRepository  = null;
        private readonly IClassificationRepository _classificationRepository = null;
        public IUserRepository User => _userRepository ?? new UserRepository(_blogDbContext);
        public IEssayRepository Essay => _essayRepository ?? new EssayRepository(_blogDbContext);
        public IEssayImageRepository EssayImage => _essayImageRepository ?? new EssayImageRepository(_blogDbContext);
        public IClassificationRepository Classification => _classificationRepository ?? new ClassificationRepository(_blogDbContext);

        public RepositoryWrapper(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
    }
}