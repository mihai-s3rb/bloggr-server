using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Models;
using Bloggr.Domain.Entities;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Messages.Queries
{
    public class GetMessagesHistoryHandler : IRequestHandler<GetMessagesHistoryQuery, CursorPagedResult>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UOW;
        private readonly IUserAccessor _userAccessor;

        public GetMessagesHistoryHandler(IMapper mapper, IUnitOfWork UOW, IUserAccessor userAccessor)
        {
            _mapper = mapper;
            _UOW = UOW;
            _userAccessor = userAccessor;
        }
        public async Task<CursorPagedResult> Handle(GetMessagesHistoryQuery request, CancellationToken cancellationToken)
        {
            var existing = await _UOW.Users.Query().Where(user => user.UserName == request.username).FirstOrDefaultAsync();
            if (existing == null)
                throw EntityNotFoundException.OfType<User>();

            var userId = _userAccessor.GetUserId();

            if (request.cursor != null && request.cursor != 0)
            {
                var cursorResult = await _UOW.Messages.Query().Where(message => ((message.SenderId == userId && message.ReceiverId == existing.Id) || (message.SenderId == existing.Id && message.ReceiverId == userId)) && message.Id <= request.cursor).Include(message => message.Sender).OrderByDescending(m => m.CreationDate).Take(11).ToListAsync();

                return CursorPaginte(cursorResult);
            }

            var result = await _UOW.Messages.Query().Where(message => (message.SenderId == userId && message.ReceiverId == existing.Id) || (message.SenderId == existing.Id && message.ReceiverId == userId)).Include(message => message.Sender).OrderByDescending(message => message.CreationDate).Take(11).ToListAsync();

            return CursorPaginte(result);
        }

        public CursorPagedResult CursorPaginte(IEnumerable<Message> messages)
        {
            int length = messages.Count();
            int? nextCursor = null;
            if(length > 10)
            {
                nextCursor = messages.Last().Id;
            }
            var pagedResult = new CursorPagedResult
            {
                NextCursor = nextCursor,
                Result = _mapper.Map<IEnumerable<MessageDto>>(messages.Take(10).OrderBy(m => m.CreationDate))
            };
            return pagedResult;
        } 
    }
}
