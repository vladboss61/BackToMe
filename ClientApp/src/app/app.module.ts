import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
//import { HttpModule } from '@angular/http';
//import { HttpClientModule } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Heroe } from "../models/Heroe";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports:
    [
      BrowserModule,
      BrowserAnimationsModule,
      //HttpClientModule,  
      //HttpClient,
      FormsModule,
      HttpModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
