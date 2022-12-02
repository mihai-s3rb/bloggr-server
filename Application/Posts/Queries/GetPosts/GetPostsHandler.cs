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
        public async Task<IEnumerable<BaseEntity>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var obj = new Post { Id = 1 };
            var list = new List<BaseEntity>();
            list.Add(obj);

            //return await _baseRepository.GetAll();
            return list;
        }
    }
}
