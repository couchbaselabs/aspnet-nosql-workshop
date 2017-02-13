<?php
$cluster = new CouchbaseCluster('couchbase://127.0.0.1');
$bucket = $cluster->openBucket('addressbook');

$bucket->upsert('d790a968-7eb7-4df8-bd5c-a3e8174d3106',
    ['first_name' => 'Laura', 'last_name' => 'Franecki', 'city' => 'Lake Ollie', 'country' => 'Palau']);
$bucket->upsert('ce0d99d9-4f1d-4d4b-b026-e030ef918ea0',
    ['first_name' => 'Carrie', 'last_name' => 'Torphy', 'city' => 'New Adolffurt', 'country' => 'Luxembourg']);
$bucket->upsert('228334d3-cf67-4bfd-92be-6b8d90a88808',
    ['first_name' => 'Kraig', 'last_name' => 'Bosco', 'city' => 'North Sheldon', 'country' => 'Mayotte']);

$query = CouchbaseN1qlQuery::fromString('SELECT * FROM `addressbook` ORDER BY first_name, last_name');
foreach ($bucket->query($query)->rows as $row) {
    $contact = $row->addressbook;
    printf("%s %s, %s, %s\n", $contact->first_name, $contact->last_name, $contact->city, $contact->country);
}