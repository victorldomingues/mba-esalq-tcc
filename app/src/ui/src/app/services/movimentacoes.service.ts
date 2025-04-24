import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovimentacoesService {

  private url = `${environment.api.movimentacoes.url}/movimentacoes`;
  constructor(private httpClient: HttpClient) { }
  movimentar(movimentacao: any): Observable<any> {
    return this.httpClient.post(this.url, movimentacao);
  }
  listarMovimentacoes(): Observable<any> {
    return this.httpClient.get(this.url);
  }
}
