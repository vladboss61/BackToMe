import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { LoggerService } from './services/logger.service';
import { LogLevle } from './businesslogic/LogLevle';
import { nameof } from './businesslogic/nameof';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Welcome to Heroes of might and magic 3';
  public values: string[];

  constructor(private _http: Http, private _logger: LoggerService) {// DI http into constructor 
    this.values = ["Render now", "Now Test", "Third Try"];
    _logger.logInfoToConsole(LogLevle.Info, "AppComponent", "Rendered");
  }    
}
