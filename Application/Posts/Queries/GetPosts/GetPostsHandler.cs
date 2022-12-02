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
        private readonly IBaseRepository<Post> _baseRepository;

        public GetPostsHandler(IBaseRepository<Post> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<IEnumerable<BaseEntity>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {

            return await _baseRepository.GetAll();
        }
    }
}
