import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Hello World and vlad starting LearnAngular';
  public sel = 'Bla-bla';
  public values: string[];

  constructor() {
    this.values = ["Render now", "Now Test", "Third Try"];
    //this.http
    //  .get('/api/values')
    //  .subscribe(result => {
    //      console.log("Request");
    //    this.values = result as string[];
    //  },
    //  error => console.error(error));
  }
}
