import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Utility } from './utility';

import { AppComponent } from './app.component';
import { ListComponent } from './list/list.component';
import { ItemComponent } from './item/item.component';

// TODO: Add Component classes for List and Item
// to the declarations
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      // TODO: set up routing:
      //  path of "" (root) should go to the List Component
      //  path of "item" (new Item) should go to the Item Component
      //  path of "item/:documentId" (existing Item) should also go to the Item Component
    ])
  ],
  providers: [Utility],
  bootstrap: [AppComponent]
})
export class AppModule { }
