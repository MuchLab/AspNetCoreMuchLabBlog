using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuchBlog.Domain.Helpers
{
    /// <summary>
    /// 页元数据列表
    /// </summary>
    public class PageList<T>:List<T>
    {
        public PageList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            AddRange(items);
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            TotalCount = totalCount;
        }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        /// <summary>
        /// 生成页元数据列表
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">每页个数</param>
        /// <returns></returns>
        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {

            var totalCount = source.Count();
            //Skip(n):忽略前n个；Take(n)：忽略后n个
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var list = new PageList<T>(items, totalCount, pageNumber, pageSize);
            return await Task.FromResult(list);
        }
    }
}