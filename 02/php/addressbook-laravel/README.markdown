# Laravel Couchbase Sample: Addressbook

Make sure that bucket `addressbook` exists on the local Couchbase cluster, and also primary N1QL index created. To
do so go to "Query Workbench" and execute this statement:

    CREATE PRIMARY INDEX on `addressbook`;

Bootstrap:

    composer install
    php artisan serve

Navigate http://localhost:8000/ and try to input some data.
