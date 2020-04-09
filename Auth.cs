using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge.Models;
using Microsoft.AspNetCore.Http;

namespace achieve_edge
{
	public static class Auth
	{
		private static Dictionary<string, string> listeners = new Dictionary<string, string>();
		public static EdgeResponse RegisterListener(string key, string domain, string listener)
		{
			EdgeResponse response = new EdgeResponse();
			if (DomainOptions.keyExist(key, domain))
			{
				listeners[domain] = listener;
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
			string val = null;
			if (!listeners.TryGetValue(domain, out val))
				return false;
			return val == listener;
		}

		public static string getListener(string domain)
		{
			if (!listeners.ContainsKey(domain))
				return null;
			return listeners[domain];
		}
	}
}
