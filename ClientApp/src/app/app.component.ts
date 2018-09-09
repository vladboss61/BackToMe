import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { Heroe } from '../models/Heroe';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Hello World and vlad starting LearnAngular';
  public static sel = 'Bla-bla';
  public values: string[];
  public heroes: Heroe[] = [];

  constructor(private _http: Http) {// DI http into constructor 
    this.values = ["Render now", "Now Test", "Third Try"];
    this.ngOnInit();     
    //this.http
    //  .get('/api/values')
    //  .subscribe(result => {
    //      console.log("Request");
    //    this.values = result as string[];
    //  },
    //  error => console.error(error));
  }  
  ngOnInit() {
    console.log("ngOnInit is activated");
    this._http.get('/api/heroes').subscribe(values => {
      this.heroes = values.json() as Heroe[];
    });
  }
  
}
