import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SaldoService {
  private url = `${environment.api.saldos.url}/saldos`;
  constructor(private httpClient: HttpClient) { }
  recuperar(): Observable<any> {
    return this.httpClient.get(this.url);
  }

}