import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
//import { HttpModule } from '@angular/http';
//import { HttpClientModule } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HeroesComponent } from './heroes/heroes.component';
import { HeroService } from './hero.service';
import { LoggerService } from './services/logger.service';

@NgModule({
  declarations: [
    AppComponent,
    HeroesComponent,
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
  providers: [LoggerService,  HeroService], //  registrate services as DI into tree of all injectable values in scope of app.module
  bootstrap: [AppComponent, HeroesComponent]
})

export class AppModule { }
