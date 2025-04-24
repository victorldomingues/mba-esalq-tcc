import { ApplicationConfig, LOCALE_ID, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import { routes } from './app.routes';
import { authInterceptor } from './interceptors/auth.interceptor';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';



import { correlationIdInterceptor } from './interceptors/correlationid.interceptor';




registerLocaleData(localePt);
export const appConfig: ApplicationConfig = {
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    //provideInstrumentation(),
    provideHttpClient(
    withInterceptors([authInterceptor, correlationIdInterceptor])
  ),provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)]

};

