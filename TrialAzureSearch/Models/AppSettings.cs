namespace TrialAzureSearch.Models
{

    public class Rootobject
    {
        public string AllowedHosts { get; set; }
        public Azuresearchconfig AzureSearchConfig { get; set; }
        public Logging Logging { get; set; }
    }

    public class Azuresearchconfig
    {
        public string SearchServiceName { get; set; }
        public string SearchServiceAdminApiKey { get; set; }
        public string SearchIndexName { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

}
