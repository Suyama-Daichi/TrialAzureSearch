using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TrialAzureSearch.Models;
using TrialAzureSearch.Services;

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
        private Azuresearchconfig _azureSearchConfig = null;
        private SearchServiceClient _serviceClient = null;
        private ISearchIndexClient _indexClient;

        public TrialAzureSearchController(ILogger<TrialAzureSearchController> logger, IOptions<Azuresearchconfig> config)
        {
            _azureSearchConfig = config.Value;
            _serviceClient = new SearchServiceClient(_azureSearchConfig.SearchServiceName, new SearchCredentials(_azureSearchConfig.SearchServiceAdminApiKey));
            _logger = logger;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult> AzureSearch(SearchData<Hotel> model, string indexName)
        {
            if (string.IsNullOrEmpty(model.searchText))
            {
                model.searchText = string.Empty;
            }

            return await RunQueryAsync(model, indexName);
        }

        [Route("searchCosmos")]
        [HttpPost]
        public async Task<ActionResult> AzureSearch(SearchData<TrialSearchCosmos> model, string indexName)
        {
            if (string.IsNullOrEmpty(model.searchText))
            {
                model.searchText = string.Empty;
            }
            return await RunQueryAsync(model, indexName);
        }


        private async Task<ActionResult> RunQueryAsync<T>(SearchData<T> model, string indexName)
        {
            var parameters = new SearchParameters
            {
                // Enter Hotel property names into this list so only these values will be returned.
                // If Select is empty, all values will be returned, which can be inefficient.
                Select = new[] { "HotelName", "Description" }
            };

            // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
            model.resultList = await _serviceClient.Indexes.GetClient(indexName).Documents.SearchAsync<T>(model.searchText);

            return Ok(model.resultList);
        }
    }
}
