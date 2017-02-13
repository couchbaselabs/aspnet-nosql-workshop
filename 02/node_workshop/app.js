'use strict';

var couchbase = require('couchbase');
var uuid = require('uuid');

// Connect to Couchbase
var cluster = new couchbase.Cluster('couchbase://localhost?fetch_mutation_tokens=true');

// Open the bucket
var bucket = cluster.openBucket('default');

// Insert some user documents
bucket.upsert(uuid.v4(), {
  'first_name': 'Laura',
  'last_name': 'Franecki',
  'city': 'Lake Ollie',
  'country': 'Palau'
}, function(err, res) {
  if (err) throw err;

  // Query for all our users
  var qs = 'SELECT * FROM `default` ORDER BY first_name, last_name';
  var q = couchbase.N1qlQuery.fromString(qs).consistency(couchbase.N1qlQuery.Consistency.REQUEST_PLUS);
  bucket.query(q, function(err, rows, meta) {
    if (err) throw err;

    for (var i in rows) {
      console.log(rows[i]);
    }

    process.exit(0);
  });
});
