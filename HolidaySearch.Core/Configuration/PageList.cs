using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidaySearch.Core.Configuration
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList(List<T> Items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(Items);
        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pagesize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pagesize);
        }

        public static async Task<PageList<T>> CreateAsync(List<T> source, int pageNumber, int pagesize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
            return new PageList<T>(items, count, pageNumber, pagesize);
        }
    }
}
