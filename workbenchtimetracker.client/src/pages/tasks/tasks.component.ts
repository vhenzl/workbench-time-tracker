import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TasksRouteState } from './tasks.resolver';
import { type Duration, secondsToDurationObj, timeStringToSeconds } from '../../utils/duration';
import { Task } from '../../api/tasks-api.service';
import { FormatDurationPipe } from '../../pipes/format-duration.pipe';

@Component({
  selector: 'app-tasks',
  imports: [FormatDurationPipe, RouterLink],
  template: `
    @switch (state.status) {
      @case ('ok') {
        <div class="list-group">
          @for (task of state.tasks; track task.id) {
            <a class="list-group-item" [routerLink]="['/tasks', task.id]">
              <div class="d-flex align-items-start justify-content-between mb-1">
                <div class="mb-0 fw-semibold flex-grow-1 me-3">
                  {{ task.title }}
                </div>
                @if (task.assigneeName) {
                  <small class="text-muted text-nowrap">
                    <i class="bi bi-person me-1"></i>{{ task.assigneeName }}
                  </small>
                }
              </div>

              <div class="d-flex align-items-end justify-content-between">
                @if (task.description) {
                  <p class="mb-0 text-muted flex-grow-1 me-3">{{ task.description }}</p>
                } @else {
                  <div class="flex-grow-1"></div>
                }
                @if (totalDuration(task) | formatDuration; as formattedDuration) {
                  <small class="fw-medium text-nowrap">
                    <i class="bi bi-clock me-1"></i>{{ formattedDuration }}
                  </small>
                }
              </div>
            </a>
          }
        </div>
      }
      @case ('error') {
        <div class="alert alert-danger">
          Failed to load tasks: {{ state.error.message }}
        </div>
      }
    }
  `,
  styles: [`
    .list-group-item:hover {
      background-color: #f8f9fa;
    }
  `]
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
