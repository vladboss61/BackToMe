import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Http } from '@angular/http';
import { Hero } from '../models/Hero';
import { LoggerService } from '../services/logger.service';
import { LogLevle } from '../businesslogic/LogLevle';
import { HeroService } from '../services/hero.service'; 

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  
  private static Hero : string ;    
  
  public heroes$: Observable<Hero[]>;
  
  constructor(private _http: Http, private _logger: LoggerService, private _heroService: HeroService)     
    {
      HeroesComponent.Hero = "Lord";
    }

  ngOnInit() {
    this._logger.logInfoToConsole(
      LogLevle.Info,
      "HeroesComponent",
      "OnInit Into HeroesComponent selected into from service.");

      this.heroes$ = this._heroService.getAllHeroes();        
  }  
}
