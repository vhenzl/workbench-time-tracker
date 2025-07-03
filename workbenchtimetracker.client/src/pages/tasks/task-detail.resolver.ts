import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Task, TasksApiService } from '../../api/tasks-api.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export type TaskDetailRouteState =
  | { status: 'ok'; task: Task }
  | { status: 'error'; error: any };

@Injectable({ providedIn: 'root' })
export class TaskDetailResolver implements Resolve<TaskDetailRouteState> {
  private tasksApi = inject(TasksApiService);

  resolve(route: ActivatedRouteSnapshot): Observable<TaskDetailRouteState> {
    const id = route.paramMap.get('id');
    if (!id) throw new Error(`Missing route param 'id'.`);
    return this.tasksApi.getTask(id).pipe(
      map(task => ({ status: 'ok', task } as const)),
      catchError(error => of({ status: 'error', error } as const))
    );
  }
}
