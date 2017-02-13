var express = require('express');
var uuid = require('uuid');
var config = require('config');
var couchbaseConfig = config.get("couchbase");

var couchbase = require('couchbase');
var cluster = TODO; // connect to a new couchbase Cluster using the couchbaseConfig
var bucket = TODO; // use the cluster to open a bucket using couchbaseConfig
var N1qlQuery = couchbase.N1qlQuery;

var router = express.Router();

router.get('/get/:id', function(req, res) {
  var id = req.params["id"];

  // TODO: if id is falsy, then return a bad request (400) with error message

  // TODO: use the bucket to get a document by id and define a callback
  // TODO: in the callback, if err is truthy: return an internal server error (500) with the error message
  // TODO: in the callback, if err is falsy: return the result value
});

router.get('/getAll', function(req, res) {
  // TODO: create a N1QL query that returns all the documents by a given type
  // TODO: use REQUEST_PLUS consistency
  var query = N1qlQuery
    .fromString("TODO: N1QL")
    .consistency("TODO: scan consistency");

  bucket.query(query, ["TODO: pass in 'person' as the parameter"], function(err, rows) {
    if (err) {
      return res.status(500).send(err);
    }
    return res.send(rows);
  });
});

router.post('/save', function(req, res) {
  if(!req.body.firstname) {
    return res.status(400).send({"status": "error", "message": "A firstname is required"});
  } else if(!req.body.lastname) {
    return res.status(400).send({"status": "error", "message": "A lastname is required"});
  } else if(!req.body.email) {
    return res.status(400).send({"status": "error", "message": "A email is required"});
  }

  var doc = {
    id: req.body.id || uuid.v4(),
    firstname: req.body.firstname,
    lastname: req.body.lastname,
    email: req.body.email,
    type: "person"
  };

  // TODO: use the bucket's upsert function to insert/update the document
  // TODO: callback to handle error

  bucket.TODO;
});

router.post('/delete', function(req, res) {
  if (!req.body.id) {
    return res.status(400).send({"status": "error", "message": "A 'id' is required"});
  }

  // TODO: use the bucket's remove function to remove the document by its key
  bucket.TODO;
});

module.exports = router;
