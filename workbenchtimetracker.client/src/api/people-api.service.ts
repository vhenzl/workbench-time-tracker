import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Person {
  id: string;
  name: string;
}

@Injectable({ providedIn: 'root' })
export class PeopleApiService {
  private readonly apiUrl = '/api/people';

  constructor(private http: HttpClient) {}

  getPeople(): Observable<Person[]> {
    return this.http.get<Person[]>(this.apiUrl);
  }
}
