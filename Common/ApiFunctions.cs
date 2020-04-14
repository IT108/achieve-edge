using achieve_edge.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace achieve_edge.Common
{
	public static class ApiFunctions
	{
		public static void DefineDomains(IConfiguration Configuration)
		{
			List<Domain> domains = new List<Domain>();

			WebRequest request = WebRequest.Create($"{Configuration["API_ADDRESS"]}domain/keys?api_key={Configuration["EDGE_API_TOKEN"]}");
			request.Method = "GET";


			try
			{
				WebResponse response;
				response = request.GetResponse();

				Stream dataStream;

				using (dataStream = response.GetResponseStream())
				{

					StreamReader reader = new StreamReader(dataStream);

					string responseFromServer = reader.ReadToEnd();

					Console.WriteLine(responseFromServer);
					domains = JsonConvert.DeserializeObject<List<Domain>>(responseFromServer);
				}

				response.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Api is inaccessible. " + ex.Message);
				Environment.Exit(1);
			}


			DomainOptions.DefineDomains(domains, Configuration);
		}
	}
}
