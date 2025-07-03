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

interface PostTimeRecordData {
  taskId: string;
  personId: string;
  date: string;
  duration: string;
}

@Injectable({ providedIn: 'root' })
export class TimeRecordsApiService {
  private readonly apiUrl = '/api/time-records';

  constructor(private http: HttpClient) {}

  postTimeRecord(timeRecord: PostTimeRecordData): Observable<TimeRecord> {
    return this.http.post<TimeRecord>(this.apiUrl, timeRecord);
  }
}
