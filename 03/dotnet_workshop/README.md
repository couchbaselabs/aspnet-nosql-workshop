# Couchbase .NET web service for workshop part 03

This uses .NET Web API to provide REST web service access to a Couchbase data store.

## Prerequites:
* Visual Studio 2015 Community or Professional
* Couchbase server with data, index, query services enabled with bucket and indexes setup

## Configuration
This by default connects to _localhost_ and a bucket called _default_. However you can change those settings in _Web.config_.

## API
* GET /api/get/{id}
* GET /api/getAll
* POST /api/save
* POST /api/delete

## Starting up the service
Debug > Start Debugging (or F5)

If that goes well, this will start a web server on and open your default browser with the home page loaded.
