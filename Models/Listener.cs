using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace achieve_edge.Models
{
	public class Listener
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[JsonProperty("domain")]
		[BsonElement("domain")]
		public string Domain { get; set; }

		[JsonProperty("client_id")]
		[BsonElement("client_id")]
		public string ClientId { get; set; }
	}
}
