import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Http } from '@angular/http';

import { Hero } from '../models/Hero';
import { LoggerService } from '../services/logger.service';
import { LogLevle } from '../businesslogic/LogLevle';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  
  private static Hero : string ;    
  public heroes: Hero[] = [];
  
  constructor(
    private _http: Http, 
    private _logger: LoggerService) {
      
      HeroesComponent.Hero = "Lord";
   }

  ngOnInit() {
    this._logger.logInfoToConsole(
      LogLevle.Info,
      "HeroesComponent",
      "OnInit Into HeroesComponent selected into from service.");
    
    this._http.get("/api/heroes").subscribe(values => {
    this._logger.logInfoToConsole(
      LogLevle.Warnin,
      "HeroesComponent", 
      "Subscribe from service.");
      this.heroes = values.json() as Hero[];
    });
  }
}
