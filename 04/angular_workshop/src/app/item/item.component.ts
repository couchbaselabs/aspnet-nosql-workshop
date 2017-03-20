import { Component, OnInit } from '@angular/core';
import { Item } from '../item';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Utility } from '../utility';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {

  public person: Item;

  public constructor(private http: Http, private route: ActivatedRoute, private location: Location, private utility: Utility) {
    this.person = new Item();
  }

  public ngOnInit() {
    // TODO: on initialization, there's either a documentId in the URL (edit) or not (add new)
    // use RxJS to subscribe to the route params
    // check to see if documentId was specified
    // if it was, then use HTTP to get the information for the specified person
    // and set the person field

    // hint 0: this.route.params is an Observable of key/value pairs
    // hint 1: you can subscribe to an Observable with the 'subscribe' method
    // hint 2: use the new lambda JS syntax, e.g. foo.subscribe(params => ...)
    // hint 3: map the results of the GET to JSON
    // hint 4: subscribe to the results of the JSON map
    // hint 5: don't forget to set the Id property of the person (if necessary)
    // hint 6: you can (optionally) subscribe to an error event and output the error information to console

  }

  public save() {
    // TODO: save the values entered on the form as a new item (or update an existing item)
    // use http to POST to the save endpoint

    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    // hint 0: you can chain together Observables using RxJS extensions
    // hint 1: use this.http.post("url to the save endpoint", "json string to post", options)
    // hint 2: map the results of that POST to json
    // hint 3: subscribe to the json output (even though we're not going to use the results in this case)
    // hint 4: use this.location to go back (to the main list)
    // hint 5: you can (optionally) subscribe to an error event and output the error information to console
  }
}
