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
