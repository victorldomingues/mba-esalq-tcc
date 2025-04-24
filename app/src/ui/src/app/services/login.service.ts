import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AUTH_INTERCEPTOR_SEM_LOGAR } from '../interceptors/auth.interceptor';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private url = `${environment.api.login.url}/login`;
  constructor(private httpClient: HttpClient) { }
  login(form: any): Observable<any> {
    return this.httpClient.post(this.url, form, {
      context: new HttpContext().set(AUTH_INTERCEPTOR_SEM_LOGAR, true)
    });
  }
}
