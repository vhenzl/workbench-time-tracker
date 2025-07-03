import { Component, inject, signal } from '@angular/core';
import { NonNullableFormBuilder, Validators, ReactiveFormsModule, AbstractControl } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PeopleApiService, Person } from '../../api/people-api.service';
import { TimeRecordsApiService, TimeRecord } from '../../api/time-records-api.service';

@Component({
  selector: 'app-time-record-create-modal',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="modal-header">
      <h5 class="modal-title">Add Time Record</h5>
      <button type="button" class="btn-close" aria-label="Close" (click)="modal.dismiss()" [disabled]="submitting()"></button>
    </div>
    @if (error()) {
      <div class="alert alert-danger m-3">
        {{ error() }}
      </div>
    }
    <form [formGroup]="form" (ngSubmit)="onSubmit()" autocomplete="off">
      <div class="modal-body">
        <div class="mb-3">
          <label for="person" class="form-label">Person</label>
          <select
            id="person"
            class="form-select"
            formControlName="personId"
          >
            <option value="" disabled selected>Select a person</option>
            @for (person of people(); track person.id) {
              <option [value]="person.id">{{ person.name }}</option>
            }
          </select>
          @if (getValidationErrorMessage('personId')) {
            <div class="invalid-feedback d-block">
              {{ getValidationErrorMessage('personId') }}
            </div>
          }
        </div>
        <div class="mb-3">
          <label for="date" class="form-label">Date</label>
          <input
            id="date"
            type="date"
            class="form-control"
            formControlName="date"
            [class.is-invalid]="form.get('date')?.invalid && form.get('date')?.touched"
            [max]="today"
          >
          @if (getValidationErrorMessage('date')) {
            <div class="invalid-feedback">
              {{ getValidationErrorMessage('date') }}
            </div>
          }
        </div>
        <div class="mb-3">
          <label for="duration" class="form-label">Time spent on the task</label>
          <input
            id="duration"
            type="time"
            class="form-control"
            formControlName="duration"
            [class.is-invalid]="form.get('duration')?.invalid && form.get('duration')?.touched"
          >
          @if (getValidationErrorMessage('duration')) {
            <div class="invalid-feedback">
              {{ getValidationErrorMessage('duration') }}
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
          Add
        </button>
      </div>
    </form>
  `
})
export class TimeRecordCreateModalComponent {
  public modal = inject(NgbActiveModal);
  private fb = inject(NonNullableFormBuilder);
  private peopleApi = inject(PeopleApiService);
  private timeRecordsApi = inject(TimeRecordsApiService);
  public taskId!: string; // To be set when opening the modal
  public assigneeId: string | null = null; // May be set when opening the modal

  today = new Date().toISOString().split('T')[0]; // for date input max attribute
  people = signal<Person[]>([]);
  submitting = signal(false);
  error = signal<string | null>(null);

  validationMessages = {
    personId: { required: 'Person is required' },
    date: { required: 'Date is required' },
    duration: {
      required: 'Duration is required',
      nonZeroTime: 'Duration must not be zero',
    },
  } as const;

  form = this.fb.group({
    personId: ['', Validators.required],
    date: ['', Validators.required],
    // defaults to 00:00 to prevent the UI preselecting the current time
    duration: ['00:00', [
      Validators.required,
      (control: AbstractControl) => {
        const value: string = control.value;
        // both "00:00" and "00:00:00" are invalid
        if (value === '00:00' || value === '00:00:00') {
          return { nonZeroTime: true };
        }
        return null;
      },
    ]]
  });

  ngOnInit() {
    if (!this.taskId) throw new Error('Property taskId is required for TimeRecordCreateModalComponent');

    if (this.assigneeId) {
     console.log('Setting initial personId from assigneeId:', this.assigneeId);
     this.form.patchValue({ personId: this.assigneeId });
    }

    this.form.disable();
    this.peopleApi.getPeople().subscribe({
      next: people => {
        this.people.set(people);
        this.form.enable();
      },
      error: () => {
        this.people.set([]);
        this.error.set('Failed to load people.');
      }
    });
  }

  onSubmit() {
    if (this.form.valid && !this.submitting()) {
      this.submitting.set(true);
      this.error.set(null);
      this.form.disable();

      // TODO: Review this getRawValue usage (form.value returns Partial<>)
      const payload = {
        ...this.form.getRawValue(),
        taskId: this.taskId,
      };
      this.timeRecordsApi.postTimeRecord(payload).subscribe({
        next: (createdTimeRecord: TimeRecord) => {
          this.submitting.set(false);
          this.modal.close(createdTimeRecord);
        },
        error: (err) => {
          this.submitting.set(false);
          this.error.set('Failed to create time record. ' + err?.error?.error);
          this.form.enable();
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
