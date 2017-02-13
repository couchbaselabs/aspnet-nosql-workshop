package couchbase.workshop;

import com.couchbase.client.java.Bucket;
import com.couchbase.client.java.Cluster;
import com.couchbase.client.java.CouchbaseCluster;
import com.couchbase.client.java.document.JsonDocument;
import com.couchbase.client.java.document.json.JsonObject;
import com.couchbase.client.java.query.N1qlParams;
import com.couchbase.client.java.query.N1qlQuery;
import com.couchbase.client.java.query.N1qlQueryResult;
import com.couchbase.client.java.query.N1qlQueryRow;
import com.couchbase.client.java.query.consistency.ScanConsistency;

import java.util.UUID;

/*
  This example demonstrates basic N1QL querying by writing a document to Couchbase and reading it using N1QL
*/

public class BasicN1QLExample {
    public static void main(String... args) throws Exception {
        // Initialize the Connection
        Cluster cluster = CouchbaseCluster.create("TODO");  // TODO: specify the URI of the couchbase cluster
        Bucket bucket = cluster.openBucket("TODO");  // TODO: get a bucket object by bucket name

        //Generate a unique id for key creation
        String uniqueID = "TODO"; // TODO: use a UUID
        String key = "p:" + uniqueID;

        // Create a JSON Document
        JsonObject person = JsonObject.create()
                .put("firstName", "John")
                .put("lastName", "Doe")
                .put("city", "San Francisco")
                .put("country", "United States")
                .put("type", "person");


        // Store the Document
        bucket.upsert(JsonDocument.create(key, person));

        // Create a N1QL Primary Index (but ignore if it exists)
        bucket.bucketManager().createN1qlPrimaryIndex(true, false);

        // Perform N1QL Query. Use request plus consistency to read your write immediately.
        // TODO: create a N1QL query that selects the key with META().id and names it "documentKey", and the rest of the document
        N1qlQuery query =  N1qlQuery.parameterized("TODO",
                JsonObject.create().put("type", "person"),
                N1qlParams.build().consistency(ScanConsistency.REQUEST_PLUS));

        N1qlQueryResult result = bucket.query(query);

        for (N1qlQueryRow row : result) {
            System.out.println("Key: " + row.value().getString("documentKey") + "\t" +
                    "FirstName: " + row.value().getString("firstName") + "\t" +
                    "LastName: " + row.value().getString("lastName") + "\t"
            );
        }
        
        cluster.disconnect();

    }
}
