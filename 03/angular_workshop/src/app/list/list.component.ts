import { Component, OnInit } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Utility } from '../utility';
import { Item } from '../item';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public people: Array<Item>;

  public constructor(private http: Http, private utility: Utility) { 
      this.people = [];
  }

  public ngOnInit() {
    // initialize the people array with the set of existing Items

    // TODO: use http to initialize the people Array
    // hint 0: you can chain together Observables using RxJS extensions
    // hint 1: use this.http.get("url to the getAll endpoint")
    // hint 2: map the results of that get to json
    // hint 3: subscribe to the json output and set the people array with the results
    // hint 4: you can (optionally) subscribe to an error event and output the error information to console
  }

   public delete(documentId: string) {
      // delete the Item with the given documentId

      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });

      // TODO: use http to POST to the delete endpoint
      // hint 0: you can chain together Observables using RxJS extensions
      // hint 1: use this.http.post("url to the delete endpoint", "json string to post", options)
      // hint 2: map the results of that POST to json
      // hint 3: subscribe to the json output and set the people array with the results (even though we're not going to use the results in this case)
      // hint 4: loop through the people variable and splice the Item that matches the deleted id
  }
}
