using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PublicAPI
{
    public class MetricsAPI
    {
		private const string _route = "metrics/";

		[FunctionName(nameof(MetricsAPI_Get))]
		public async Task<IActionResult> MetricsAPI_Get(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = _route + "Get")] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			ActionResult result = new OkObjectResult("");

			return result;
		}
	}
}
