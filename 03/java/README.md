# Couchbase Java webservice for workshop part 03

This uses spring boot and spring data couchbase to provide RESTful access to 
couchbase data store. 

## Prerequisites
* Java 7 or later
* Maven 3 or later
* Couchbase server with data, index, query services enabled with bucket and 
indexes setup

## Configuration
This by default connects to localhost and a bucket called person. However you 
can change those settings in application properties.

## API
* GET /api/getAll
* GET /api/get/{id}
* POST /api/save
* POST /api/delete

## Starting up the service
Run command
```
mvn spring-boot:run
```
If that goes well, this will start a web server on `127.0.0.1:8080`. 

## Usage through curl
curl -H "Content-Type: application/json" -XPOST http://localhost:8080/api/save 
-d '{"firstName":"John","lastName":"Doe","email":"john@foo.bar"}'

{"status":"ok"}%

curl -H "Content-Type: application/json" -XGET 
http://localhost:8080/api/get\?id\=4ec14320-3eaa-47ab-82a4-27dee874a1a1

{"results":{"id":"4ec14320-3eaa-47ab-82a4-27dee874a1a1","Version":570653436280832,
"firstName":"John","lastName":"Doe","email":"john@foo.bar"},"status":"ok"}%

curl -H "Content-Type: application/json" -XGET http://localhost:8080/api/getAll

{"results":{"persons":[{"id":"4ec14320-3eaa-47ab-82a4-27dee874a1a1",
"Version":570653436280832,"firstName":"John","lastName":"Doe","email":"john@foo.bar"}]},
"status":"ok"}%

curl -H "Content-Type: application/json"  -XPOST http://localhost:8080/api/delete 
-d '{"id": "c295dbb4-f2f7-4143-a1e2-cab716269e35"}'

{"status":"ok"}%
