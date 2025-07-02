import { Injectable, inject } from '@angular/core';
import { Resolve } from '@angular/router';
import { Task, TasksApiService } from '../../api/tasks-api.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export type TasksRouteState =
  | { status: 'ok'; tasks: Task[] }
  | { status: 'error'; error: any };

@Injectable({ providedIn: 'root' })
export class TasksResolver implements Resolve<TasksRouteState> {
  private tasksApi = inject(TasksApiService);

  resolve(): Observable<TasksRouteState> {
    return this.tasksApi.getTasks().pipe(
      map(tasks => ({ status: 'ok', tasks } as const)),
      catchError(error => of({ status: 'error', error } as const))
    );
  }
}
