using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using achieve_edge.Models;

namespace achieve_edge.Services
{
	public class ListenerService
	{
		private readonly IMongoCollection<Listener> _listeners;

		public ListenerService(IEdgeDBSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_listeners = database.GetCollection<Listener>(settings.ListenerCollectionName);
		}

		public List<Listener> Get() => _listeners.Find(listener => true).ToList();

		public Listener Get(string id) => _listeners.Find<Listener>(listener => listener.Id == id).FirstOrDefault();
		public Listener Get(Listener listenerIn) => _listeners.Find<Listener>(listener => listener.Domain == listenerIn.Domain).FirstOrDefault();

		public Listener GetByDomain(string name) => _listeners.Find<Listener>(listener => listener.Domain == name).FirstOrDefault();

		public Listener Create(Listener listener)
		{
			_listeners.InsertOne(listener);
			return listener;
		}

		public void Update(string id, Listener listenerIn) => _listeners.ReplaceOne(domain => domain.Id == id, listenerIn);

		public void Remove(Listener listenerIn) => _listeners.DeleteOne(listener => listener.Id == listenerIn.Id);

		public void Remove(string id) => _listeners.DeleteOne(listener => listener.Id == id);
	}
}
