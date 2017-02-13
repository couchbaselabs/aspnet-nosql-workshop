# SDK Sample PHP

Make sure that bucket `addressbook` exists on the local Couchbase cluster, and also primary N1QL index created. To
do so go to "Query Workbench" and execute this statement:

    CREATE PRIMARY INDEX on `addressbook`;

## Bare PHP with Couchbase

Install Couchbase PHP SDK using getting started guide:
http://developer.couchbase.com/documentation/server/current/sdk/php/start-using-sdk.html

Initialize connection:

``` php
$cluster = new CouchbaseCluster('couchbase://127.0.0.1');
$bucket = $cluster->openBucket('addressbook');

```

Store documents into the bucket:

``` php
$bucket->upsert('d790a968-7eb7-4df8-bd5c-a3e8174d3106',
    ['first_name' => 'Laura', 'last_name' => 'Franecki', 'city' => 'Lake Ollie', 'country' => 'Palau']);
$bucket->upsert('ce0d99d9-4f1d-4d4b-b026-e030ef918ea0',
    ['first_name' => 'Carrie', 'last_name' => 'Torphy', 'city' => 'New Adolffurt', 'country' => 'Luxembourg']);
$bucket->upsert('228334d3-cf67-4bfd-92be-6b8d90a88808',
    ['first_name' => 'Kraig', 'last_name' => 'Bosco', 'city' => 'North Sheldon', 'country' => 'Mayotte']);
```

Query documents with N1QL:

``` php
$query = CouchbaseN1qlQuery::fromString('SELECT * FROM `addressbook` ORDER BY first_name, last_name');
foreach ($bucket->query($query)->rows as $row) {
    $contact = $row->addressbook;
    printf("%s %s, %s, %s\n", $contact->first_name, $contact->last_name, $contact->city, $contact->country);
}
```


## Laravel project with Couchbase extension

Install Laravel 5.3 using official guide:
https://laravel.com/docs/5.3/installation

Install Couchbase PHP SDK using getting started guide:
http://developer.couchbase.com/documentation/server/current/sdk/php/start-using-sdk.html

Generate project skeleton:

    laravel new addressbook

Navigate to project directory:

    cd addressbook

Add repository (temporarily) to `composer.json`:

``` json
"repositories": [
    {
        "type": "vcs",
        "url": "https://github.com/avsej/laravel-couchbase"
    }
]
```

Add package to `composer.json`:

``` json
"require": {
    ...
    "ytake/Laravel-Couchbase": "dev-master"
}
```

Install packages:

    composer install

Register service provider in `config/app.php`:

``` php
'providers' => [
    // ...
    // all other providers...

    \Ytake\LaravelCouchbase\CouchbaseServiceProvider::class,
],
```

Add connection settings to `config/database.php`:

``` php
'couchbase' => [
    'driver' => 'couchbase',
    'host' => 'couchbase://127.0.0.1',
],
```

Generate home controller:

    php artisan make:controller ContactController

Expose it as `/home` in `routes/web.php`:

``` php
Route::resource('contacts', 'ContactController');
Route::get('/', function() { return redirect()->route('contacts.index'); });
```

The contents of the `app/Http/Controllers/ContactController.php` should be

``` php
<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

use Ramsey\Uuid\Uuid;

class ContactController extends Controller
{
    /**
     * Couchbase connection.
     *
     * @var \Ytake\LaravelCouchbase\Database\CouchbaseConnection
     */
    protected $db;

    /**
     * Create a new ContactController instance.
     *
     * @return void
     */
    public function __construct()
    {
        $this->db = DB::connection('couchbase');
    }

    /**
     * Display a listing of the contacts.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        // Select REQUEST_PLUS consistency to show that just inserted
        // contact appears in list after redirect, it will cost us some latency,
        // so if it is not required -- drop consistency restriction.
        $collection = $this->db->consistency(\CouchbaseN1qlQuery::REQUEST_PLUS)->
                    table('addressbook')->orderBy('first_name')->orderBy('last_name')->get();
        $contacts = [];
        foreach ($collection as $item) {
            array_push($contacts, $item->addressbook);
        }
        return view('contacts.index', ['contacts' => $contacts]);
    }

    /**
     * Store a newly created contact in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        $this->db->table('addressbook')->key(Uuid::uuid1())->upsert($request->except('_token'));

        return redirect()->route('contacts.index');
    }
}
```

The contents of `resources/views/contacts/index.blade.php`:

``` html
<!DOCTYPE html>
<html lang="en">
    <body>
        <h1>Couchbase Addressbook</h1>
        <ol>
            @forelse ($contacts as $contact)
                <li>{{ $contact->first_name }} {{ $contact->last_name }},
                    {{ $contact->city }}, {{ $contact->country }}</li>
            @empty
                No contacts.
            @endforelse
        </ol>

        <form action="{{ route('contacts.store')}}" method="POST">
            <input type="text" name="first_name" placeholder="First Name">
            <input type="text" name="last_name" placeholder="Last Name">
            <br>
            <input type="text" name="city" placeholder="City">
            <input type="text" name="country" placeholder="Country">
            <br>
            <input type="hidden" name="_token" value="{{ csrf_token() }}">
            <input type="submit" value="Save">
        </form>
    </body>
</html>
```

The last step is to run the web server:

    php artisan serve

And navigate your browser to http://localhost:8000 and try to add some data into the system.

![Screenshot of Laravel sample](screenshot.png)
