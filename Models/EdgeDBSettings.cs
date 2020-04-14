using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace achieve_edge.Models
{
	public class EdgeDBSettings : IEdgeDBSettings
	{
		public string ListenerCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}

	public interface IEdgeDBSettings
	{
		public string ListenerCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}
