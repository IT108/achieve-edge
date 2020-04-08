using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge;

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
			await Clients.Caller.SendAsync("Register", Auth.RegisterListener(key, domain, Clients.Caller.ToString()).ToString());
		}
	}
}
