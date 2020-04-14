using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge.Models;
using achieve_edge.Services;
using Microsoft.AspNetCore.Http;

namespace achieve_edge
{
	public static class Auth
	{
		private static ListenerService _listeners;

		public static void Init(ListenerService listener)
		{
			_listeners = listener;
		}

		public static EdgeResponse RegisterListener(string key, string domain, string listener)
		{
			EdgeResponse response = new EdgeResponse();
			if (DomainOptions.keyExist(key, domain))
			{
				updateListener(domain, listener);
				response.status = StatusCodes.Status200OK;
				response.message = "Succesfully registered";
			}
			else
			{
				response.status = StatusCodes.Status403Forbidden;
				response.message = "Invalid edge key";
			}
			return response;
		}

		public static bool isRegistered(string listener, string domain)
		{
			Listener val = _listeners.GetByDomain(domain);
			if (val == null)
				return false;
			return val.ClientId == listener;
		}

		public static Listener getListener(string domain)
		{
			return _listeners.GetByDomain(domain);
		}

		public static void removeListener(string domain)
		{
			Listener val = _listeners.GetByDomain(domain);
			if (val != null)
				_listeners.Remove(val.Id);
		}

		private static void updateListener(string domain, string listener_id)
		{
			Listener listener = new Listener() { ClientId = listener_id, Domain = domain };

			var DBListener = _listeners.Get(listener);
			if (DBListener != null)
			{
				listener.Id = DBListener.Id;
				_listeners.Update(DBListener.Id, listener);
			}
			else
				_listeners.Create(listener);

		}
	}
}
