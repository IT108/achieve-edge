using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge.Models;

namespace achieve_edge
{
	public static class Auth
	{
		private static Dictionary<string, string> listeners = new Dictionary<string, string>();
		public static bool RegisterListener(string key, string domain, string listener)
		{
			if (DomainOptions.keyExist(key, domain))
			{
				listeners[domain] = listener;
				return true;
			}
			return false;
		}

		public static bool isRegistered(string listener, string domain)
		{
			string val = null;
			listeners.TryGetValue(domain, out val);
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
