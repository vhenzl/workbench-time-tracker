import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    NgbCollapse,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './layout.component.html',
  styles: [`
    main.container { min-height: 80vh; }
  `]
})
export class LayoutComponent {
  isCollapsed = true;
}
