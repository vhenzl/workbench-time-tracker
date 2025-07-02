import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TasksRouteState } from './tasks.resolver';
import { type Duration, secondsToDurationObj, timeStringToSeconds } from '../../utils/duration';
import { Task } from '../../api/tasks-api.service';
import { FormatDurationPipe } from '../../pipes/format-duration.pipe';

@Component({
  selector: 'app-tasks',
  imports: [FormatDurationPipe],
  template: `
    @switch (state.status) {
      @case ('ok') {
        <table class="table">
          <thead>
            <tr>
              <th>Title</th>
              <th>Description</th>
              <th>Assignee</th>
              <th>Time</th>
            </tr>
          </thead>
          <tbody>
            @for (task of state.tasks; track task.id) {
              <tr>
                <td>{{ task.title }}</td>
                <td>{{ task.description }}</td>
                <td>{{ task.assigneeName }}</td>
                <td>{{ totalDuration(task) | formatDuration }}</td>
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

  totalDuration(input: Task): Duration {
    const totalSeconds = input.timeRecords
      .map(record => record.duration)
      .map(timeStringToSeconds)
      .reduce((sum: number, val: number) => sum + val, 0);
    return secondsToDurationObj(totalSeconds);
  }
}
