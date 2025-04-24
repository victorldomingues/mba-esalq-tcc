import { Component } from '@angular/core';
import { HeaderComponent } from '../../layout/header/header.component';
import { RouterLinkActive, RouterLink } from '@angular/router';
import { SaldoComponent } from '../../components/saldo/saldo.component';
import { ListaMovimentacoesComponent } from '../../components/lista-movimentacoes/lista-movimentacoes.component';
@Component({
  selector: 'app-movimentacoes',
  imports: [HeaderComponent,RouterLink, SaldoComponent, ListaMovimentacoesComponent],
  templateUrl: './movimentacoes.component.html',
  styleUrl: './movimentacoes.component.scss'
})
export class MovimentacoesComponent {

}
