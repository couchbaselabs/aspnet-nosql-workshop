using System;
using System.Collections.Generic;
using System.Configuration;
using Couchbase;
using Couchbase.Configuration.Client;

namespace workshop_dotnet
{
    public static class CouchbaseConfig
    {
        public static void Setup()
        {
            var config = new ClientConfiguration
            {
                Servers = new List<Uri> {new Uri(ConfigurationManager.AppSettings.Get("couchbaseServer"))}
            };
            ClusterHelper.Initialize(config);
        }

        public static void CleanUp()
        {
            ClusterHelper.Close();
        }
    }
}