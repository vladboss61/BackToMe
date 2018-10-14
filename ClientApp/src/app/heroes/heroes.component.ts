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
  
  public hero$: Observable<Hero>;

  public isWatched : boolean;
  
  constructor(private _http: Http, private _logger: LoggerService, private _heroService: HeroService)     
  {
    this.isWatched = false;
      HeroesComponent.Hero = "Lord";
  }

  ngOnInit() {
    this._logger.logInfoToConsole(
      LogLevle.Info,
      "HeroesComponent",
      "OnInit Into HeroesComponent selected into from service.");

      this.heroes$ = this._heroService.getAllHeroes();       
  }

  inputIdHandler($event)
  {
    let targetHero = $event.target.value;
    let id = Number.parseInt(targetHero);
    
    if(targetHero !== "" && !Number.isNaN(id))
    {      
        this.hero$ = this._heroService.getHeroById(id)        
        this.isWatched = true;      
    }
    else
    {
      this.isWatched = false;
    }    
  }
}
