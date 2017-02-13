import { Component } from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';
import { Utility } from './utility';

@Component({
  selector: 'app-root',
  template: '<div style="padding: 10px"><router-outlet></router-outlet></div>',
  directives: [ROUTER_DIRECTIVES],
  providers: [Utility]
})
export class AppComponent {
  title = 'app works!';
}
