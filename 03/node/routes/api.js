var express = require('express');
var uuid = require('uuid');
var config = require('config');
var couchbaseConfig = config.get("couchbase");

var couchbase = require('couchbase');
var cluster = new couchbase.Cluster(couchbaseConfig.server);
var bucket = cluster.openBucket(couchbaseConfig.bucket);
var N1qlQuery = couchbase.N1qlQuery;

var router = express.Router();

router.get('/get/:id', function(req, res) {
  var id = req.params["id"];
  if (!id) {
    return res.status(400).send({"status": "error", "message": "A 'id' is required"});
  }

  bucket.get(id, function(err, result) {
    if (err) {
      return res.status(500).send(err);
    }
    return res.send(result.value);
  });
});

router.get('/getAll', function(req, res) {
  var query = N1qlQuery
    .fromString("SELECT `default`.* FROM `default` WHERE type = $1")
    .consistency(N1qlQuery.Consistency.REQUEST_PLUS);

  bucket.query(query, ["person"], function(err, rows) {
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
  bucket.upsert(doc.id, doc, function(err, result) {
    if (err) {
      return res.status(500).send(err);
    }
    return res.status(200).send(doc);
  });
});

router.post('/delete', function(req, res) {
  if (!req.body.id) {
    return res.status(400).send({"status": "error", "message": "A 'id' is required"});
  }

  bucket.remove(req.body.id, function(err, result) {
    if (err) {
      return res.status(500).send(err);
    }
    return res.status(200).send(req.body);
  });
});

module.exports = router;
