import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TasksRouteState } from './tasks.resolver';
import { type Duration, secondsToDurationObj, timeStringToSeconds } from '../../utils/duration';
import { Task } from '../../api/tasks-api.service';
import { FormatDurationPipe } from '../../pipes/format-duration.pipe';
import { Title } from '@angular/platform-browser';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskCreateModalComponent } from './task-create-modal.component';

@Component({
  selector: 'app-tasks',
  imports: [FormatDurationPipe, RouterLink],
  template: `
    @switch (state.status) {
      @case ('ok') {
        <div class="d-flex justify-content-between align-items-start mb-3">
          <h2 class="flex-grow-1 me-3 mb-2">Tasks</h2>
          <button class="btn btn-primary text-nowrap" (click)="openCreateModal()">
            <i class="bi bi-plus me-1"></i>Add Task
          </button>
        </div>

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
export class TasksComponent implements OnInit {
  private title = inject(Title);
  private route = inject(ActivatedRoute);
  private modalService = inject(NgbModal);
  private router = inject(Router);
  state: TasksRouteState = this.route.snapshot.data['state'];

  ngOnInit() {
    this.title.setTitle('Tasks | Time Tracker');
  }

  totalDuration(input: Task): Duration {
    const totalSeconds = input.timeRecords
      .map(record => record.duration)
      .map(timeStringToSeconds)
      .reduce((sum: number, val: number) => sum + val, 0);
    return secondsToDurationObj(totalSeconds);
  }

  openCreateModal() {
    const modalRef = this.modalService.open(TaskCreateModalComponent, { centered: true });
    modalRef.result
      .then((createdTask: Task) => {
        if (createdTask) {
          this.router.navigate([createdTask.id], { relativeTo: this.route });
        }
      })
      .catch(() => {
        // Modal dismissed, do nothing
      });
  }
}
