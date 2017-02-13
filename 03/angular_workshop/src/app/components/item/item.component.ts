import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ROUTER_DIRECTIVES } from '@angular/router';
import { Location } from '@angular/common';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Utility } from '../../utility';

@Component({
    templateUrl: './app/components/item/item.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class ItemComponent implements OnInit {

    public person: any;

    public constructor(private http: Http, private route: ActivatedRoute, private location: Location, private utility: Utility) {
        this.person = {
            "firstname": "",
            "lastname": "",
            "email": ""
        }
    }

    public ngOnInit() {
        this.route.params.subscribe(params => {
            if(params["documentId"]) {
                // TODO: make an http GET request to the api/get endpoint with documentId
                // remember that this.utility contains the base url to the RESTful services
                this.http.get("TODO")
                    .map(result => result.json())
                    .subscribe(results => {
                        this.person = results;
                        this.person.document_id = params["documentId"];
                    }, error => {
                        console.error(error);
                    });
            }
        });
    }

    public save() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        // TODO: make an http POST request to the api/save endpoint with a JSON string of the person
        this.http.post("TODO: url", "TODO: json of this.person", options)
            .map(result => result.json())
            .subscribe(results => {
                this.location.back();
            }, error => {
                console.error(error);
            });
    }

}
