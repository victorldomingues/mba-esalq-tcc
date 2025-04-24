import { Component } from '@angular/core';
import { SaldoService } from '../../services/saldo.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-saldo',
  imports: [CommonModule],
  templateUrl: './saldo.component.html',
  styleUrl: './saldo.component.scss'
})
export class SaldoComponent {
  saldo: any;
  constructor(private saldoService: SaldoService) { }


  ngOnInit() {
    this.saldoService.recuperar().subscribe(resposta => {
      this.saldo = resposta;
    });
  }
}
