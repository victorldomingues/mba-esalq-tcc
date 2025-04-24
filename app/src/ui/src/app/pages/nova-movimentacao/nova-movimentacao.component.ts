import { Component, inject } from '@angular/core';

import { HeaderComponent } from '../../layout/header/header.component';
import { RouterLinkActive, RouterLink, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import { MovimentacoesService } from '../../services/movimentacoes.service';
import { CommonModule } from '@angular/common';
import { SaldoComponent } from '../../components/saldo/saldo.component';

@Component({
  selector: 'app-nova-movimentacao',
  imports: [HeaderComponent, RouterLink, ReactiveFormsModule, CommonModule, SaldoComponent],
  templateUrl: './nova-movimentacao.component.html',
  styleUrl: './nova-movimentacao.component.scss'
})
export class NovaMovimentacaoComponent {
  constructor(private router: Router) { }
  private formBuilder = inject(FormBuilder);
  private movimentacoesService = inject(MovimentacoesService);
  form = this.formBuilder.group({
    valor: [, [Validators.required, Validators.min(0.1)]],
    tipo: [1, Validators.required],
    forma: [1, Validators.required],
    destinatario: ['', Validators.required],
    banco: [''],
    agencia: [''],
    conta: [''],
    dac: [''],
  });

  enviar() {
    if (this.form.valid) {
      const movimentacao = this.form.value;
      movimentacao.forma = parseInt(`${this.form.value.forma}`);
      movimentacao.tipo = parseInt(`${this.form.value.tipo}`);
      if(movimentacao.forma === 3) {
        
        movimentacao.banco = undefined;
        movimentacao.agencia = undefined;
        movimentacao.conta = undefined;
        movimentacao.dac = undefined;
      }
      this.movimentacoesService.movimentar(movimentacao).subscribe(resposta => {
        this.router.navigate(['/movimentacoes']);
      });
    }
  }
}
