using Bloggr.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Services
{
    public class PagedResult<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IList<TEntity> Result { get; set; }

        public static async Task<PagedResult<TEntity>> FromAsync(IQueryable<TEntity> query, PageModel pageDto)
        {
            var pagedResult = new PagedResult<TEntity>();
            var totalCount = await query.CountAsync();
            pagedResult.TotalCount = totalCount;

            pagedResult.Result = await Paginate(query, pageDto).ToListAsync();
            pagedResult.PageNumber = pageDto.PageNumber;
            pagedResult.PageSize = pageDto.PageSize;
            pagedResult.TotalPages = (totalCount / pagedResult.PageSize) + (totalCount % pagedResult.PageSize == 0 ? 0 : 1);

            return pagedResult;
        }

        private static IQueryable<TEntity> Paginate(IQueryable<TEntity> query, PageModel pageDto)
        {
            return query.Skip((pageDto.PageNumber - 1) * pageDto.PageSize).Take(pageDto.PageSize);
        }
    }
}
