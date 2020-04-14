using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace achieve_edge.Models
{
	public class Domain
	{
		public Domain(string domainName, string displayName, string key)
		{
			DomainName = domainName;
			Key = key;
			DisplayName = displayName;
		}

		[Required]
		[JsonProperty("domain")]
		public string DomainName { get; set; }

		[Required]
		[JsonProperty("display_name")]
		public string DisplayName { get; set; }

		[Required]
		[JsonIgnore]
		[JsonProperty("key")]
		public string Key { get; set; }
	}
}
