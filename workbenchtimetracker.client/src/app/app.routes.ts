import { Routes } from '@angular/router';
import { LayoutComponent } from '../layout/layout.component';
import { PeopleResolver } from '../pages/people/people.resolver';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'tasks', pathMatch: 'full' },
      {
        path: 'people',
        loadComponent: () => import('../pages/people/people.component').then(m => m.PeopleComponent),
        resolve: { state: PeopleResolver }
      },
      { path: 'tasks', loadComponent: () => import('../pages/tasks/tasks.component').then(m => m.TasksComponent) },
      { path: 'tasks/:id', loadComponent: () => import('../pages/tasks/task-detail.component').then(m => m.TaskDetailComponent) },
    ]
  },
];
