import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Hero } from '../models/Hero';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  
  private static Hero : string ;  
  public heroes: Hero[] = [];
  
  constructor(private _http: Http) {
    HeroesComponent.Hero = "Lord"
   }

  ngOnInit() {
    this._http.get("/api/heroes").subscribe(values => {
      this.heroes = values.json() as Hero[];
    });
  }
}
