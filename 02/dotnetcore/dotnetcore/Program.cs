using System;
using System.Collections.Generic;
using System.IO;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.N1QL;

namespace DotnetExample
{
    class Program
    {
        private static IBucket _bucket;

        static void Main(string[] args)
        {
            InitializeCouchbase();

            // create a new document in Couchbase Server
            var document = new Document<dynamic>
            {
                Id = Guid.NewGuid().ToString(),
                Content = new
                {
                    firstName = "Connie",
                    lastName = Path.GetRandomFileName(), // used to get random string
                    city = Path.GetRandomFileName() + ", Ohio",
                    country = "The United States of " + Path.GetRandomFileName(),
                    type = "person"
                }
            };
            _bucket.Insert(document);
            Console.WriteLine($"Inserted document, key: {document.Id}");

            // query all the keys and documents using N1QL
            var queryText = "SELECT META(p).id AS documentKey, p.* FROM `default` AS p WHERE type='person'";
            var query = QueryRequest.Create(queryText);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = _bucket.Query<dynamic>(query);
            foreach (var doc in result.Rows)
            {
                Console.WriteLine("----------");
                Console.WriteLine($"Key: {doc.documentKey}");
                Console.WriteLine($"\tfirstName: {doc.firstName}");
                Console.WriteLine($"\tlastName: {doc.lastName}");
                Console.WriteLine($"\tcity: {doc.city}");
                Console.WriteLine($"\tcountry: {doc.country}");
            }

            CloseCouchbase();
        }

        private static void InitializeCouchbase()
        {
            var config = new ClientConfiguration();
            config.Servers = new List<Uri> {
                new Uri("couchbase://localhost")
            };
            ClusterHelper.Initialize(config);
            _bucket = ClusterHelper.GetBucket("default");
        }

        private static void CloseCouchbase()
        {
            ClusterHelper.Close();
        }
    }
}
