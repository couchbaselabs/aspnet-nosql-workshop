# Couchbase java N1QL Example

This workshop example uses couchbase sdk to create a document and query it through N1QL service. 

Installation
* Java 6 or higher
* Maven

This example uses couchbase-java-client version 2.3.3. It uses a bucket named "default".

To run the example

    mvn compile
    mvn exec:java -D exec.mainClass="couchbase.workshop.BasicN1QLExample"  
       
