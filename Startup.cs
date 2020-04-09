using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using achieve_edge.Hubs;
using achieve_edge.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace achieve_edge
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			DefineDomains();
			services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
			services.AddControllers();
			services.AddSignalR();
		}

		public void DefineDomains()
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

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<InternalHub>("/internal");
			});
		}
	}
}
