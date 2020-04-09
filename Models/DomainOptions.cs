using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace achieve_edge.Models
{
	public static class DomainOptions
	{
		private static IConfiguration Configuration;

		public static string EdgeAddress { get; set; }
		public static string EdgeAPIToken { get; set; }

		private static Dictionary<string, Domain> domains = new Dictionary<string, Domain>();

		public static IEnumerable<Domain> Domains
		{
			get {
				return domains.Values;
			}
		}

		public static void DefineDomains(List<Domain> domainModels, IConfiguration configuration)
		{
			Configuration = configuration;

			EdgeAddress = configuration["API_ADDRESS"];
			EdgeAPIToken = configuration["EDGE_API_TOKEN"];

			if (domains.Count != 0)
				throw new AccessViolationException("Domains already defined");
			foreach (Domain domain in domainModels)
			{
				domains.Add(domain.Key, domain);
			}
		}

		public static bool keyExist(string domainName, string key)
		{
			Domain val = null;
			domains.TryGetValue(domainName, out val);
			return val.Key == key;
		}
	}
}
