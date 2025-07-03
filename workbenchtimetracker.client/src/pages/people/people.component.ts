import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { type PeopleRouteState } from './people.resolver';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-people',
  template: `
    @switch (state.status) {
      @case ('ok') {
        <table class="table">
          <thead>
            <tr><th>Name</th></tr>
          </thead>
          <tbody>
            @for (person of state.people; track person.id) {
              <tr>
                <td>{{ person.name }}</td>
              </tr>
            }
          </tbody>
        </table>
      }
      @case ('error') {
        <div class="alert alert-danger">
          Failed to load people: {{ state.error.message }}
        </div>
      }
    }
  `
})
export class PeopleComponent implements OnInit {
  private title = inject(Title);
  private route = inject(ActivatedRoute);
  state: PeopleRouteState = this.route.snapshot.data['state'];

  ngOnInit() {
    this.title.setTitle('People | Time Tracker');
  }
}
