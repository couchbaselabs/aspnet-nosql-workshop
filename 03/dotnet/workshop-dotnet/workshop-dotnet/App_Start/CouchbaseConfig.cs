using System;
using System.Collections.Generic;
using System.Configuration;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;

namespace workshop_dotnet
{
    public static class CouchbaseConfig
    {
        public static Cluster Cluster => _cluster;
        private static Cluster _cluster;

        public static void Setup()
        {
            var url = ConfigurationManager.AppSettings.Get("couchbaseServer");
            var username = ConfigurationManager.AppSettings.Get("couchbaseUsername");
            var password = ConfigurationManager.AppSettings.Get("couchbasePassword");

            var config = new ClientConfiguration
            {
                Servers = new List<Uri> {new Uri(url) }
            };
            _cluster = new Cluster(config);
            _cluster.Authenticate(new PasswordAuthenticator(username, password));
        }

        public static void CleanUp()
        {
            _cluster.Dispose();
        }
    }
}