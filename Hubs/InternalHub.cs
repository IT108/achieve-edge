using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using achieve_edge.Models;
using achieve_edge.Services;
using achieve_lib.AD;
using System.Collections.Generic;
using achieve_lib;

namespace achieve_edge.Hubs
{
	public class InternalHub : Hub
	{
		private static Dictionary<string, EdgeRequest> requests = new Dictionary<string, EdgeRequest>();
		private readonly ListenerService _listeners;

		public InternalHub(ListenerService listeners)
		{
			_listeners = listeners;
		}

		public async Task Register(string key, string domain)
		{
			string caller = Context.ConnectionId;
			await Clients.Caller.SendAsync("RegisterResponse", Auth.RegisterListener(key, domain, caller));
		}

		public async Task GetUser(ADAuthRequest req)
		{
			if (req.ApiKey != DomainOptions.EdgeAPIToken)
			{
				req.IsSuccess = false;
				req.Error = "API key not valid";
				await Clients.Caller.SendAsync("UserInfo", req);
			}

			req.Caller = Context.ConnectionId;
			Listener client = _listeners.GetByDomain(req.Domain);
			await Clients.Client(client.ClientId).SendAsync("GetUserInfo", req);
		}

		public async Task UserInfo(ADAuthRequest response)
		{
			if (response.IsSuccess)
				Console.WriteLine(response.Answer.Username);
			else
				Console.WriteLine(response.Error);
			await Clients.Client(response.Caller).SendAsync("UserInfo", response);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			Auth.removeListenerByConn(Context.ConnectionId);
			await base.OnDisconnectedAsync(exception);
		}
	}
}
