import { Component } from '@angular/core';

@Component({
  selector: 'app-portal-footer',
  template: `
    <footer class="portal-footer">
      <span>Games Portal — migrated from <code>game-portal</code> (single Angular 20 app)</span>
    </footer>
  `,
  styles: [
    `
      .portal-footer {
        margin-top: 2rem;
        padding-top: 1rem;
        border-top: 1px solid rgba(255, 255, 255, 0.08);
        font-size: 0.85rem;
        opacity: 0.7;
      }
    `,
  ],
})
export class PortalFooterComponent {}
