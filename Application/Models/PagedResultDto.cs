using AutoMapper;
using Bloggr.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models
{
    public class PagedResultDto<TViewModel> where TViewModel : class
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public List<TViewModel> Result { get; set; }

        public static PagedResultDto<TViewModel> From<TEntity>(PagedResult<TEntity> pagedResult, IMapper mapper) where TEntity : class
        {
            var result = new PagedResultDto<TViewModel>
            {
                Result = mapper.Map<List<TViewModel>>(pagedResult.Result),
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber,
                TotalPages = pagedResult.TotalPages
            };
            return result;
        }

    }
}