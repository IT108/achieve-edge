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
		public Domain(string domainName, string key)
		{
			DomainName = domainName;
			Key = key;
		}

		[Required]
		[JsonProperty("domain")]
		public string DomainName { get; set; }

		[Required]
		[JsonIgnore]
		public string Key { get; set; }
	}
}
