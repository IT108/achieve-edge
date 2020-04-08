using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using achieve_edge.Models;

namespace achieve_edge.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DomainController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetDomains()
		{
			return Ok(Models.DomainOptions.Domains);
		}
	}
}
