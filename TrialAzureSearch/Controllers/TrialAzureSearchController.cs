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
        public async Task<DocumentSearchResult<Hotel>> AzureSearch(SearchData<Hotel> model, string indexName)
        {
            if (string.IsNullOrEmpty(model.searchText))
            {
                model.searchText = string.Empty;
            }
            return await new SearchService(_serviceClient).RunQueryAsync(model, indexName);
        }

        [Route("searchCosmos")]
        [HttpPost]
        public async Task<DocumentSearchResult<TrialSearchCosmos>> AzureSearch(SearchData<TrialSearchCosmos> model, string indexName)
        {
            if (string.IsNullOrEmpty(model.searchText))
            {
                model.searchText = "*";
            }
            return await new SearchService(_serviceClient).RunQueryAsync(model, indexName);
        }

        //[Route("updateIndex")]
        //[HttpPost]
        //public async Task<ActionResult> UpdateIndex(string indexName)
        //{
        //}


    }
}
