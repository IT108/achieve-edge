using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using achieve_edge.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using achieve_edge.Hubs;
using achieve_lib.AD;

namespace achieve_edge.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DomainController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetDomains([Required] [FromQuery] string api_key)
		{
			if (api_key != DomainOptions.EdgeAPIToken)
				return StatusCode(StatusCodes.Status401Unauthorized, "Wrong api key");
			return Ok(DomainOptions.Domains);
		}

		[HttpGet]
		[Route("[controller]/auth")]
		public IActionResult Auth([Required] [FromQuery] ADAuthRequest request)
		{
			if (request.ApiKey != DomainOptions.EdgeAPIToken)
				return StatusCode(StatusCodes.Status401Unauthorized, "Wrong api key");
			return Ok(DomainOptions.Domains);
		}
	}
}
