import { Component, OnInit, inject } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {

  ngOnInit() {
    console.log('Init AppComponent');
  }

  title = 'Workbench Time Tracker';
}
