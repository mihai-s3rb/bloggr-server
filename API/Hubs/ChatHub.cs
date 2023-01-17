using Bloggr.Application.Interfaces;
using Bloggr.Domain.Entities;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bloggr.WebUI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _UOW;
        private readonly IUserAccessor _userAccessor;

        public ChatHub(IUnitOfWork UOW, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _userAccessor = userAccessor;
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        public async Task NewMessage(string username, string message)
        {
            var userName = _userAccessor.GetUserNameOrNull();
            System.Console.WriteLine(userName);
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public async Task SendMessageToUser(string sendToUserName, string message)
        {
            var senderId = Context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (senderId == null)
                throw new CustomException("Not able to authenticate you");

            var existing = await _UOW.Users.Query().Where(user => user.UserName.Equals(sendToUserName)).FirstOrDefaultAsync();
            if (existing == null)
                throw EntityNotFoundException.OfType<User>();

            var messageDto = new Message
            {
                SenderId = Int32.Parse(senderId),
                ReceiverId = existing.Id,
                Content = message
            };

            await _UOW.Messages.Add(messageDto);
            await Clients.Group(sendToUserName).SendAsync("messageReceived", Context.User.Identity.Name, message);
            await _UOW.Save();
        }
    }
}
