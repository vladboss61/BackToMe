import { Injectable } from '@angular/core';
import { LogLevle} from '../businesslogic/LogLevle';


@Injectable({
  providedIn: 'root'
})
export class LoggerService {  
  constructor() { }

  public logInfoToConsole(
    loglevle: LogLevle, 
    source : string, 
    informaion : string): void {
      console.log(`${LogLevle[loglevle]} : ${source} :  ${informaion}`);  
  }
  //private log(...args: string[]){ /*test only test*/}
}
