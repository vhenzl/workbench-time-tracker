import { Routes } from '@angular/router';
import { LayoutComponent } from '../layout/layout.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'tasks', pathMatch: 'full' },
      { path: 'people', loadComponent: () => import('../pages/people/people.component').then(m => m.PeopleComponent) },
      { path: 'tasks', loadComponent: () => import('../pages/tasks/tasks.component').then(m => m.TasksComponent) },
      { path: 'tasks/:id', loadComponent: () => import('../pages/tasks/task-detail.component').then(m => m.TaskDetailComponent) },
    ]
  },
];
