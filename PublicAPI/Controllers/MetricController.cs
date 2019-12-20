using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Handlers;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PublicAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MetricController : ControllerBase
	{
		private readonly ILogger<MetricController> _logger;

		public MetricController(ILogger<MetricController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{id}")]
		public IEnumerable<Metric> Get(int id)
		{
			return null;
		}
	}
}
