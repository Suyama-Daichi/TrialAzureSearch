using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrialAzureSearch.Models
{
    public class TrialSearchCosmos
    {
        public string id { get; set; }
        public string category { get; set; }
        public string fileName { get; set; }
        public string thumbnail { get; set; }
        public int registDate { get; set; }
        [IsFilterable]
        public string[] brand { get; set; }
        public string[] product { get; set; }
        public string[] area { get; set; }
    }
}
