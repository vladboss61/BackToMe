import { Injectable } from '@angular/core';
import { Observable} from 'rxjs'
import { ajax } from 'rxjs/ajax';
import { map, catchError, filter, tap } from 'rxjs/operators';
import { Hero } from '../models/Hero';

@Injectable({
  providedIn: 'root'
})
export class HeroService 
{
  constructor() { }

  getAllHeroes(): Observable<Hero[]>{
    return ajax("/api/heroes").pipe(       
       // map get Select from response, and it is a Hero[] but in next operator we are filtering all heroes where name eq to "Vlad"
      map(response  => (response.response as Hero[]).filter(hero => hero.name == "Vlad")),      
    );
  }

  getHeroById(id: number): Observable<Hero>{
    return ajax(`/api/heroes/${id}`).pipe(
      map(respose => respose.response as Hero))
  } 
}