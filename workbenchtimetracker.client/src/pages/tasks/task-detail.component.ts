import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskDetailRouteState } from './task-detail.resolver';
import { Duration, secondsToDurationObj, timeStringToSeconds } from '../../utils/duration';
import { Task } from '../../api/tasks-api.service';
import { FormatDurationPipe } from '../../pipes/format-duration.pipe';

// @Component({
//   selector: 'app-task-detail',
//   template: `Task detail for id: {{ id }}`,
// })
// export class TaskDetailComponent {
//   private route = inject(ActivatedRoute);
//   id = this.route.snapshot.paramMap.get('id');
// }


@Component({
  selector: 'app-task-detail',
  imports: [FormatDurationPipe],
  template: `
    @switch (state.status) {
      @case ('ok') {
        <h2>{{ state.task.title }}</h2>
        <p>{{ state.task.description }}</p>
        <div>
          <strong>Assignee:</strong>
          {{ state.task.assigneeName ?? '-' }}
        </div>
        <div>
          <strong>Total Time:</strong>
          {{ totalDuration(state.task) | formatDuration }}
        </div>
        <hr>
        <h4>Time Records</h4>
        <table class="table">
          <thead>
            <tr>
              <th>Date</th>
              <th>Person</th>
              <th>Time</th>
            </tr>
          </thead>
          <tbody>
            @for (record of state.task.timeRecords; track record.id) {
              <tr>
                <td>{{ record.date }}</td>
                <td>{{ record.personName }}</td>
                <td>{{ duration(record.duration) | formatDuration }}</td>
              </tr>
            }
          </tbody>
        </table>
      }
      @case ('error') {
        <div class="alert alert-danger">
          Failed to load task: {{ state.error.message }}
        </div>
      }
    }
  `,
})
export class TaskDetailComponent {
  private route = inject(ActivatedRoute);
  state: TaskDetailRouteState = this.route.snapshot.data['state'];

  totalDuration(input: Task): Duration {
    const totalSeconds = input.timeRecords
      .map(record => record.duration)
      .map(timeStringToSeconds)
      .reduce((sum: number, val: number) => sum + val, 0);
    return secondsToDurationObj(totalSeconds);
  }

  duration(input: string): Duration {
    return secondsToDurationObj(timeStringToSeconds(input));
  }
}
