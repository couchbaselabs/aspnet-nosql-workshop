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
    this.http.get(this.utility.host + "/api/getAll")
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
      this.http.post(this.utility.host + "/api/delete", JSON.stringify({id: documentId}), options)
          .map(result => result.json())
          .subscribe(results => {
              for(let i = 0; i < this.people.length; i++) {
                  if(this.people[i].Id == documentId) {
                      this.people.splice(i, 1);
                      break;
                  }
              }
          }, error => {
              console.error(error);
          });
  }
}
