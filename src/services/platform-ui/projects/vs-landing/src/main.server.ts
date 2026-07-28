import { bootstrapApplication } from '@angular/platform-browser';
import { VsLandingAppComponent } from './app/vs-landing.app.component';
import { config } from './app/vs-landing.app.config.server';

const bootstrap = () => bootstrapApplication(VsLandingAppComponent, config);

export default bootstrap;
