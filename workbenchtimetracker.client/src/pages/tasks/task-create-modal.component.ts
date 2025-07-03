import { Component, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TasksApiService, Task } from '../../api/tasks-api.service';

@Component({
  selector: 'app-task-create-modal',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="modal-header">
      <h5 class="modal-title">Create Task</h5>
      <button type="button" class="btn-close" aria-label="Close" (click)="modal.dismiss()" [disabled]="submitting()"></button>
    </div>
    <form [formGroup]="form" (ngSubmit)="onSubmit()" autocomplete="off">
      <div class="modal-body">
        @if (error()) {
          <div class="alert alert-danger mb-3">
            {{ error() }}
          </div>
        }
        <div class="mb-3">
          <label for="title" class="form-label">Title</label>
          <input
            id="title"
            type="text"
            class="form-control"
            formControlName="title"
            [class.is-invalid]="form.get('title')?.invalid && form.get('title')?.touched"
          >
          @if (form.get('title')?.errors?.['required']) {
            <div class="invalid-feedback">
              Title is required
            </div>
          }
        </div>
        <div class="mb-3">
          <label for="description" class="form-label">Description</label>
          <textarea
            id="description"
            class="form-control"
            formControlName="description"
            rows="3"
          ></textarea>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" (click)="modal.dismiss()" [disabled]="submitting()">Cancel</button>
        <button type="submit" class="btn btn-primary" [disabled]="form.invalid || submitting()">
          @if (submitting()) {
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
          }
          Create
        </button>
      </div>
    </form>
  `
})
export class TaskCreateModalComponent {
  form: FormGroup;
  submitting = signal(false);
  error = signal<string | null>(null);

  constructor(
    private fb: FormBuilder,
    public modal: NgbActiveModal,
    private tasksApi: TasksApiService
  ) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['']
    });
  }

  onSubmit() {
    if (this.form.valid && !this.submitting()) {
      this.submitting.set(true);
      this.error.set(null);
      this.tasksApi.postTask(this.form.value).subscribe({
        next: (createdTask: Task) => {
          this.submitting.set(false);
          this.modal.close(createdTask);
        },
        error: (err) => {
          this.submitting.set(false);
          this.error.set('Failed to create task. ' + err?.error?.error);
        }
      });
    }
  }
}
