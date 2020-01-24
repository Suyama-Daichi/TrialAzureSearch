using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TrialAzureSearch.Models;

namespace TrialAzureSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrialAzureSearchController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TrialAzureSearchController> _logger;
        private Azuresearchconfig _appsettings = null;

        public TrialAzureSearchController(ILogger<TrialAzureSearchController> logger, IOptions<Azuresearchconfig> options)
        {
            _appsettings = options.Value;
            _logger = logger;
        }

        [Route("search")]
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> AzureSearch()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
