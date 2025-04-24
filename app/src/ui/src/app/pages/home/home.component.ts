import { Component, inject } from '@angular/core';
import { HeaderComponent } from '../../layout/header/header.component';
import { RouterLinkActive, RouterLink } from '@angular/router';
import { SaldoService } from '../../services/saldo.service';
import { SaldoComponent } from '../../components/saldo/saldo.component';
import { ListaMovimentacoesComponent } from '../../components/lista-movimentacoes/lista-movimentacoes.component';

@Component({
  selector: 'app-home',
  imports: [HeaderComponent, RouterLink, SaldoComponent, ListaMovimentacoesComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
 
}
