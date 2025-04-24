import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkActive, RouterLink } from '@angular/router';
import { datadogRum } from '@datadog/browser-rum'

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLinkActive, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'esalq-ui';
  /**
   *
   */
  constructor() {
    datadogRum.init({
      applicationId: '96f03932-8952-4bb4-9417-316eae89e4bf',
      clientToken: 'pub532f7da085c0d19ffbfb3fb3f026cc6d',
      // `site` refers to the Datadog site parameter of your organization
      // see https://docs.datadoghq.com/getting_started/site/
      site: 'us5.datadoghq.com',
      service: 'ui',
      env: 'prd',
      version: '1.0.0',
      allowedTracingUrls: ["http://localhost", /http:\/\/.*\.localhost/, (url) => url.startsWith("https://localhost")],
      sessionSampleRate: 100,
      sessionReplaySampleRate: 100,
      trackResources: true,
      trackLongTasks: true,
      trackUserInteractions: true,
      enablePrivacyForActionName: true,
    });


  }

  ngOnInit() {
    datadogRum.addAction("aplicação iniciada");
  }
  ngOnDestroy() {
    datadogRum.stopSession();
  }
}
