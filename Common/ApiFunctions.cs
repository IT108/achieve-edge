using achieve_edge.Models;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
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

			string requestString = $"{Configuration["API_ADDRESS"]}/domain/keys?api_key={Configuration["EDGE_API_TOKEN"]}";

			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(requestString);
			request.Method = "GET";


			try
			{
				HttpWebResponse response;
				response = (HttpWebResponse) request.GetResponse();

				if (response.StatusCode != HttpStatusCode.OK)
					throw new ArgumentException("API connection error. Status code: " + response.StatusCode);

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
				Console.WriteLine(requestString);
				Console.WriteLine("Api connection error. " + ex.Message);
				Console.WriteLine(request);
				Environment.Exit(1);
			}


			DomainOptions.DefineDomains(domains, Configuration);
		}
	}
}
