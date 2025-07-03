import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TaskDetailRouteState } from './task-detail.resolver';
import { Duration, secondsToDurationObj, timeStringToSeconds } from '../../utils/duration';
import { Task } from '../../api/tasks-api.service';
import { FormatDurationPipe } from '../../pipes/format-duration.pipe';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-task-detail',
  imports: [FormatDurationPipe, DatePipe],
  template: `
    @switch (state.status) {
      @case ('ok') {
        <div class="row">
          <div class="col-12">
            <div class="d-flex justify-content-between align-items-start mb-3">
              <div class="flex-grow-1 me-3">
                <h2 class="mb-2">{{ state.task.title }}</h2>
                @if (state.task.description) {
                  <p class="text-muted mb-2">{{ state.task.description }}</p>
                }
                @if (state.task.assigneeName) {
                  <div class="mb-0">
                    <small class="text-muted">
                      <i class="bi bi-person me-1"></i>{{ state.task.assigneeName }}
                    </small>
                  </div>
                }
              </div>
              <button class="btn btn-link btn-sm text-nowrap" disabled>
                <i class="bi bi-pencil me-1"></i>Edit Task
              </button>
            </div>

            <hr class="mb-4">

            @if (state.task.timeRecords.length > 0) {
              <div class="d-flex justify-content-between align-items-center mb-1">
                <h4 class="mb-0">Time Records</h4>
                <button class="btn btn-primary btn-sm" disabled>
                  <i class="bi bi-plus-lg me-1"></i>Record Time
                </button>
              </div>

              <div class="mb-3 small">
                <span class="text-muted">Total Time: </span>
                <span class="fw-semibold">{{ totalDuration(state.task) | formatDuration }}</span>
              </div>

              <div class="table-responsive">
                <table class="table table-hover">
                  <thead class="table-light">
                    <tr>
                      <th scope="col">Date</th>
                      <th scope="col">Person</th>
                      <th scope="col">Time</th>
                      <th scope="col" class="text-end">Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (record of state.task.timeRecords; track record.id) {
                      <tr>
                        <td>{{ record.date | date:'mediumDate' }}</td>
                        <td>{{ record.personName }}</td>
                        <td>{{ duration(record.duration) | formatDuration }}</td>
                        <td class="text-end">
                          <div class="btn-group btn-group-sm">
                            <button class="btn btn-outline-secondary btn-xs" title="Edit" disabled>
                              <i class="bi bi-pencil"></i>
                            </button>
                            <button class="btn btn-outline-danger btn-xs" title="Delete" disabled>
                              <i class="bi bi-trash"></i>
                            </button>
                          </div>
                        </td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
            } @else {
              <div class="text-center py-5">
                <div class="text-muted mb-3">
                  <i class="bi bi-clock-history display-4"></i>
                </div>
                <h5 class="text-muted mb-2">No time records yet</h5>
                <p class="text-muted mb-3">Start tracking time to see records here</p>
                <button class="btn btn-primary" disabled>
                  <i class="bi bi-plus-lg me-1"></i>Record Time
                </button>
              </div>
            }
          </div>
        </div>
      }
      @case ('error') {
        <div class="alert alert-danger">
          Failed to load task: {{ state.error.message }}
        </div>
      }
    }
  `,
  styles: [`
    .table th {
      border-top: none;
      font-weight: 600;
    }

    .btn-xs {
      padding: 0.125rem 0.375rem;
      font-size: 0.75rem;
      line-height: 1.2;
    }

    .btn-xs i {
      font-size: 0.75rem;
    }
  `]
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
