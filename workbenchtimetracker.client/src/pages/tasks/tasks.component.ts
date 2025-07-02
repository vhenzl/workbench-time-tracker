import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TasksRouteState } from './tasks.resolver';

@Component({
  selector: 'app-tasks',
  template: `
    @switch (state.status) {
      @case ('ok') {
        <table class="table">
          <thead>
            <tr>
              <th>Title</th>
              <th>Description</th>
              <th>Assignee</th>
              <th>Time Records</th>
            </tr>
          </thead>
          <tbody>
            @for (task of state.tasks; track task.id) {
              <tr>
                <td>{{ task.title }}</td>
                <td>{{ task.description }}</td>
                <td>{{ task.assigneeName }}</td>
                <td>{{ task.timeRecords.length }}</td>
              </tr>
            }
          </tbody>
        </table>
      }
      @case ('error') {
        <div class="alert alert-danger">
          Failed to load tasks: {{ state.error.message }}
        </div>
      }
    }
  `
})
export class TasksComponent {
  private route = inject(ActivatedRoute);
  state: TasksRouteState = this.route.snapshot.data['state'];
}
