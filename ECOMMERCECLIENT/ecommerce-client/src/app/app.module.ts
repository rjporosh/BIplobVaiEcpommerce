import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import{ FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { from } from 'rxjs';
import { ProductEntryComponent } from './product-entry/product-entry.component';
import {HttpClientModule} from '@angular/common/http';
import { ProductInfoComponent } from './product-info/product-info.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductEntryComponent,
    ProductInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
