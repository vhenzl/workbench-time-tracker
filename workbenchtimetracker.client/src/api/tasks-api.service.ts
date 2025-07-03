import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TimeRecord {
    id: string;
    taskId: string;
    personId: string;
    personName: string;
    date: string;
    duration: string;
}

export interface Task {
    id: string;
    title: string;
    description: string;
    assigneeId: string | null;
    assigneeName: string | null;
    timeRecords: TimeRecord[];
}

@Injectable({ providedIn: 'root' })
export class TasksApiService {
  private readonly apiUrl = '/api/tasks';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  getTask(id: string): Observable<Task> {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }
}
