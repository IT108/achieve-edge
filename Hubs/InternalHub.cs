using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge;
using achieve_edge.Models;
using Microsoft.AspNetCore.Http;

namespace achieve_edge.Hubs
{
	public class InternalHub : Hub
	{
		public async Task Send(string name, string message)
		{
			await Clients.All.SendAsync("broadcastMessage", name, message);
		}	

		public async Task Register(string key, string domain)
		{
			string caller = Context.ConnectionId;
			await Clients.Caller.SendAsync("RegisterResponse", Auth.RegisterListener(key, domain, caller));
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			Auth.removeListenerByConn(Context.ConnectionId);
			await base.OnDisconnectedAsync(exception);
		}
    }
}
