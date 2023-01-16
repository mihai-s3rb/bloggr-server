using Bloggr.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Bloggr.WebUI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUserAccessor _userAccessor;

        public ChatHub(IUserAccessor userAccessor)
        {
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
            await Clients.Group(sendToUserName).SendAsync("messageReceived", Context.User.Identity.Name, message);
        }
    }
}
