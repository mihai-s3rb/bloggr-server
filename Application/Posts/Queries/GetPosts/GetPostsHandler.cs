using Bloggr.Domain.Interfaces;
using Domain.Abstracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPosts
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<BaseEntity>>
    {
        private readonly IBaseRepository<BaseEntity> _baseRepository;

        public GetPostsHandler(IBaseRepository<BaseEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task<IEnumerable<BaseEntity>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return _baseRepository.GetAll();
        }
    }
}
