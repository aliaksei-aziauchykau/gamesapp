import { bootstrapApplication } from '@angular/platform-browser';
import { VsLandingAppComponent } from './app/vs-landing.app.component';
import { appConfig } from './app/vs-landing.app.config';

bootstrapApplication(VsLandingAppComponent, appConfig).catch((err) => console.error(err));
