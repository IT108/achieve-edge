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
using Microsoft.AspNetCore.Http.Connections;
using achieve_edge.Common;
using achieve_edge.Services;
using Microsoft.Extensions.Options;

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
			SetupDB();

			services.Configure<EdgeDBSettings>(
				Configuration.GetSection(nameof(EdgeDBSettings)));

			services.AddSingleton<IEdgeDBSettings>(sp =>
				sp.GetRequiredService<IOptions<EdgeDBSettings>>().Value);

			services.AddSingleton<ListenerService>();

			Auth.Init(services.BuildServiceProvider().GetService<ListenerService>());

			ApiFunctions.DefineDomains(Configuration);
			services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
			services.AddControllers();
			services.AddSignalR();
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
				endpoints.MapHub<InternalHub>("/internal", options =>
				{
					options.Transports =
						HttpTransportType.WebSockets;
				});
			});
		}
		private void SetupDB()
		{
			Configuration["EdgeDBSettings:ConnectionString"] = Configuration["DB_CONN_STRING"];
		}
	}
}
