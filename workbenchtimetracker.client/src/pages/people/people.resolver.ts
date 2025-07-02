import { inject, Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { PeopleApiService, type Person } from '../../api/people-api.service';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export type PeopleRouteState =
  | { status: 'ok'; people: Person[] }
  | { status: 'error'; error: any };

@Injectable({ providedIn: 'root' })
export class PeopleResolver implements Resolve<PeopleRouteState> {
  private peopleApi = inject(PeopleApiService);
  resolve(): Observable<PeopleRouteState> {
    return this.peopleApi.getPeople().pipe(
      map(people => ({ status: 'ok', people } as const)),
      catchError(error => of({ status: 'error', error } as const))
    );
  }
}
