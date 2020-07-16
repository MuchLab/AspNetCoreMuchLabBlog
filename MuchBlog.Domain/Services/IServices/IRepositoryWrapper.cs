using MuchBlog.Domain.Services.IServices;

namespace MuchBlog.Domain.Services.IService
{
    public interface IRepositoryWrapper
    {
        public IUserRepository User { get; }
        public IEssayRepository Essay { get; }
        public IEssayImageRepository EssayImage { get; }
        public IClassificationRepository Classification { get; }
    }
}