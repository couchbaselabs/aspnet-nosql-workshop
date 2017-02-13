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
