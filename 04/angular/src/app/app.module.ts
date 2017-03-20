import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Utility } from './utility';

import { AppComponent } from './app.component';
import { ListComponent } from './list/list.component';
import { ItemComponent } from './item/item.component';

@NgModule({
  declarations: [
    AppComponent,
    ListComponent,
    ItemComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      { path: "", component: ListComponent },
      { path: 'item', component: ItemComponent },
      { path: 'item/:documentId', component: ItemComponent }
    ])
  ],
  providers: [Utility],
  bootstrap: [AppComponent]
})
export class AppModule { }
