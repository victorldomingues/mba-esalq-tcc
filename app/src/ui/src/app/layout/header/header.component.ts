import { Component, inject, Input } from '@angular/core';
import { RouterLinkActive, RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CadastroService } from '../../services/cadastro.service';
@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Input() menuAtivo: string = 'home';
  private cadastroService = inject(CadastroService)
  perfil: any = {
    nome: ''
  };
  constructor(private router: Router) {
    const localPerfil = localStorage.getItem('perfil');
    if (localPerfil) {
      this.perfil = JSON.parse(localPerfil);
      return;
    }

    this.cadastroService.recuperarPerfil().subscribe(perfil => {
      this.perfil = perfil;
      localStorage.setItem('perfil', JSON.stringify(perfil));
    })
  }

  sair() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('perfil');
    this.router.navigate(['/login']);
  }

}
