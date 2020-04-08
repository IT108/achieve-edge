using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace achieve_edge.Models
{
	public static class DomainOptions
	{
		private static Dictionary<string, string> keys = new Dictionary<string, string>();
		public static List<Domain> Domains = new List<Domain>();

		public static void SetKeys(Dictionary<string, string> tempKeys)
		{
			if (keys.Count != 0)
				throw new AccessViolationException();
			keys = tempKeys;
			foreach (var key in keys)
			{
				Domains.Add(new Domain(key.Key, key.Value));
			}
		}

		public static bool keyExist(string key, string domain)
		{
			string val = null;
			var success = keys.TryGetValue(domain, out val);
			return val == key;
		}
	}
}
