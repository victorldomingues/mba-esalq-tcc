import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLinkActive, RouterLink, Router } from '@angular/router';
import { CadastroService } from '../../services/cadastro.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  imports: [RouterLinkActive, RouterLink, ReactiveFormsModule],
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent {
  private cadastroService = inject(CadastroService)
  private formBuilder = inject(FormBuilder);
  private router = inject(Router);
  form = this.formBuilder.group({
    nome: ['', Validators.required],
    cpf: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    senha: ['', Validators.required],
    confirmaSenha: ['', Validators.required]
  });
  enviar() {
    if (this.form.invalid) return;
    this.cadastroService.cadastrar(this.form.value).subscribe(sucesso => {
      this.router.navigate(['/login'])
    })
  }
}
