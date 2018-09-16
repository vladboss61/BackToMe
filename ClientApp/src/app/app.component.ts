import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Hello World and vlad starting LearnAngular';
  public static sel = 'Bla-bla';
  public values: string[];

  constructor(private _http: Http) {// DI http into constructor 
    this.values = ["Render now", "Now Test", "Third Try"];    
  }    
}
