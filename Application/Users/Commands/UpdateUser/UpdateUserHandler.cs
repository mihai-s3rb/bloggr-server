using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Domain.Entities;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IAzureStorage _storage;

        public UpdateUserHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor, IAzureStorage storage)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _storage = storage;
        }

        public async Task<UsersQueryDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();

            
            //create new user !!!!
            var userFromDb = await _UOW.Users.Query().AsNoTracking().Where(user => user.Id == userId).FirstOrDefaultAsync();

            _mapper.Map<UpdateUserDto, User>(request.user, userFromDb);

            var newInterests = JsonConvert.DeserializeObject<IEnumerable<InterestQueryDto>>(request.user.Interests);
            var list = new List<InterestUser>();
            if (newInterests != null && newInterests.Any())
            {
                foreach (InterestQueryDto interest in newInterests)
                {
                    list.Add(new InterestUser
                    {
                        InterestId = interest.Id
                    });
                }
            }
            if (request.user.Profile != null)
            {
                try
                {
                    var result = await _storage.UploadAsync(request.user.Profile);
                    userFromDb.ProfileImageUrl = result.Blob.Uri;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Could not upload profile image");
                }
            }

            if (request.user.Background != null)
            {
                try
                {
                    var result = await _storage.UploadAsync(request.user.Background);
                    userFromDb.BackgroundImageUrl = result.Blob.Uri;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Could not upload background image");
                }
            }

            userFromDb.InterestUsers = list;
            await _UOW.Users.Update(userFromDb);
            await _UOW.Save();

            var updatedUser = await _UOW.Users.Query().Include(user => user.InterestUsers).ThenInclude(interestUser => interestUser.Interest).Where(user => user.Id == userId).FirstOrDefaultAsync();
            var mappedResult = _mapper.Map<UsersQueryDto>(updatedUser);
            return mappedResult;
        }
    }
}
