# Couchbase NodeJS web service for workshop part 03

This uses NodeJS and Express to provide REST web service access to a Couchbase data store.

## Prerequites:
* npm 3 or later
* node 6 or later
* Couchbase server with data, index, query services enabled with bucket and indexes setup

## Configuration
This by default connects to _localhost_ and a bucket called _default_. However you can change those settings in _config/default.json_.

## API
* GET /api/get/{id}
* GET /api/getAll
* POST /api/save
* POST /api/delete

## Starting up the service
To install dependencies, run the following:
```
npm install
```

To run the web service, run the following:
```
gulp
```

If that goes well, this will start a web server on `localhost:3000`.
