using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrialAzureSearch.Models;

namespace TrialAzureSearch.Services
{
    public class SearchService
    {
        private SearchServiceClient _serviceClient;
        public SearchService(SearchServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<DocumentSearchResult<T>> RunQueryAsync<T>(SearchData<T> model, string indexName)
        {
            var parameters = new SearchParameters
            {
                IncludeTotalResultCount = true,
                Facets = new string[] { "brand", "area" },
                // Enter Hotel property names into this list so only these values will be returned.
                // If Select is empty, all values will be returned, which can be inefficient.
                //Select = new[] { "HotelName", "Description" }
            };

            // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
            model.resultList = await _serviceClient.Indexes.GetClient(indexName).Documents.SearchAsync<T>(model.searchText, parameters);

            return model.resultList;
        }
    }
}
