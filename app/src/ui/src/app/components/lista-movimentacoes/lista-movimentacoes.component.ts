import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MovimentacoesService } from '../../services/movimentacoes.service';

@Component({
  selector: 'app-lista-movimentacoes',
  imports: [CommonModule],
  templateUrl: './lista-movimentacoes.component.html',
  styleUrl: './lista-movimentacoes.component.scss'
})
export class ListaMovimentacoesComponent {
  @Input() limit: any = undefined;
  movimentacoes: any[] = [];
  carregando = false;
  tipo = new Map([
    ['1', 'Deposito'],
    ['2', 'Saque']
  ]);
  forma = new Map([
    ['1', 'DOC'],
    ['2', 'TED'],
    ['3', 'Pix']
  ]);
  constructor(private movimentacoesService: MovimentacoesService) {
    console.log('Movimentações', this.movimentacoes);


  }

  ngOnInit() {

    this.carregando = true;
    this.movimentacoesService.listarMovimentacoes().subscribe((movimentacoes: any[]) => {
      this.carregando = false;
      let data = movimentacoes;
      if (this.limit)
        data = movimentacoes.slice(0, this.limit);
      const listaDias = new Set(data.map(movimentacao => new Date(movimentacao.criadoEm).toLocaleDateString("pt-BR")));
      listaDias.forEach((dia) => {
        this.movimentacoes.push({
          dia: dia,
          movimentacoes: data.filter(movimentacao => new Date(movimentacao.criadoEm).toLocaleDateString("pt-BR") === dia)
        });
      })
    },
      error => {
        this.carregando = false;
        console.error('Erro ao listar movimentações', error);
      });
  }
}
