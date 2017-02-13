import { Component, OnInit } from '@angular/core';
import { Router, ROUTER_DIRECTIVES } from '@angular/router';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Utility } from '../../utility';

@Component({
    templateUrl: './app/components/list/list.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class ListComponent implements OnInit {

    public people: Array<any>;

    public constructor(private http: Http, private utility: Utility) {
        this.people = [];
    }

    public ngOnInit() {
        // TODO: make an http GET request to the api/getAll endpoint
        // remember that this.utility contains the base url to the RESTful services
        this.http.get("TODO")
            .map(result => result.json())
            .subscribe(results => {
                this.people = results;
            }, error => {
                console.error(error);
            });
    }

    public delete(documentId: string) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        // TODO: make an http POST request to the api/delete endpoint with a JSON string of an object containing the 'id' field
        this.http.post("TODO", "TODO", options)
            .map(result => result.json())
            .subscribe(results => {
                for(let i = 0; i < this.people.length; i++) {
                    if(this.people[i].id == documentId) {
                        this.people.splice(i, 1);
                        break;
                    }
                }
            }, error => {
                console.error(error);
            });
    }

}
