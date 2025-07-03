import { Component, inject, signal } from '@angular/core';
import { Validators, ReactiveFormsModule, NonNullableFormBuilder } from '@angular/forms';
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
          @if (getValidationErrorMessage('title')) {
            <div class="invalid-feedback">
              {{ getValidationErrorMessage('title') }}
            </div>
          }
        </div>
        <div class="mb-3">
          <label for="description" class="form-label">
            Description <small class="text-muted">(optional)</small>
          </label>
          <textarea
            id="description"
            class="form-control"
            formControlName="description"
            rows="3"
            [class.is-invalid]="form.get('description')?.invalid && form.get('description')?.touched"
          ></textarea>
          @if (getValidationErrorMessage('description')) {
            <div class="invalid-feedback">
              {{ getValidationErrorMessage('description') }}
            </div>
          }
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
  public modal = inject(NgbActiveModal);
  private fb = inject(NonNullableFormBuilder);
  private tasksApi = inject(TasksApiService);

  submitting = signal(false);
  error = signal<string | null>(null);

  validationMessages = {
    title: {
      required: 'Title is required',
      maxlength: 'Title must be at most 200 characters'
    },
    description: {
      maxlength: 'Description must be at most 1000 characters'
    }
  } as const;

  form = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    description: ['', [Validators.maxLength(1000)]]
  });

  onSubmit() {
    if (this.form.valid && !this.submitting()) {
      this.submitting.set(true);
      this.error.set(null);
      // TODO: Review this getRawValue usage (form.value returns Partial<>)
      this.tasksApi.postTask(this.form.getRawValue()).subscribe({
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

  getValidationErrorMessage(controlName: keyof typeof this.validationMessages): string | null {
    const control = this.form.get(controlName);
    if (control?.errors) {
      for (const errorKey in this.validationMessages[controlName]) {
        if (control.errors[errorKey]) {
          return this.validationMessages[controlName][errorKey as keyof typeof this.validationMessages[typeof controlName]];
        }
      }
    }
    return null;
  }
}
