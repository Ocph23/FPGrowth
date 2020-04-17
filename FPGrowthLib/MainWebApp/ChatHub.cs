using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MainWebApp {
    [Authorize]
    public class ChatHub : Hub {

        private static List<UserConnection> users = new List<UserConnection> ();

        public async Task SendMessage (string userId, object message) {
            var connection = users.Where (x => x.UserId == userId).FirstOrDefault ();
            if (connection != null) {
                await Clients.Client (connection.ConnectionId).SendAsync ("ReceiveMessage", message);
            } else {
                await Clients.Client (Context.ConnectionId).SendAsync ("ErrorMessage", "User Tidak Online");
            }
        }

        public override Task OnConnectedAsync () {
            string userId = Context.User.FindFirst (ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty (userId)) {
                var user = users.Where (x => x.UserId == userId).FirstOrDefault ();
                if (user == null) {
                    users.Add (new UserConnection { UserId = userId, ConnectionId = Context.ConnectionId });
                } else {
                    user.ConnectionId = Context.ConnectionId;
                }
            }
            return base.OnConnectedAsync ();
        }

        public override Task OnDisconnectedAsync (Exception exception) {

            var user = users.Where (x => x.ConnectionId == Context.ConnectionId).FirstOrDefault ();
            if (user != null)
                users.Remove (user);
            return Task.CompletedTask;
        }
    }

    class UserConnection {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}