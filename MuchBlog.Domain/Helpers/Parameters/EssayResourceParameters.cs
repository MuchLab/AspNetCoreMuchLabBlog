namespace MuchBlog.Domain.Helpers.Parameters
{
    public class EssayResourceParameters
    {
        //最大页数限制
        private const int MaxPageSize = 50;
        //页数，默认值为10
        private int _pageSize = 10;
        //每页个数，默认值为1
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}