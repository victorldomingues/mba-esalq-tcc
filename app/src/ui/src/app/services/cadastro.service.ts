import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {

  private cadadstroUrl = `${environment.api.cadastro.url}/cadastros`;
  private perfilUrl = `${environment.api.cadastro.url}/perfis`;
  constructor(private httpClient: HttpClient) { }

  recuperarPerfil(): Observable<any> {
    return this.httpClient.get(this.perfilUrl);
  }

  cadastrar(cadastro: any): Observable<any> {
    return this.httpClient.post(this.cadadstroUrl, cadastro);
  }

}
