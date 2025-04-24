import { Component, inject } from '@angular/core';
import { RouterLinkActive, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../../services/login.service';
import {Router} from '@angular/router'

@Component({
  selector: 'app-login',
  imports: [RouterLinkActive, RouterLink, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private router: Router){

  }
  private formBuilder = inject(FormBuilder);
  private loginService = inject(LoginService);
  form = this.formBuilder.group({
    cpf: ['92853202011', Validators.required],
    senha: ['senha', Validators.required]
  });
  enviar() {
    if (this.form.valid) {
      this.loginService.login(this.form.value).subscribe(resposta=> {
        localStorage.setItem('accessToken', resposta.accessToken);
        this.router.navigate(['/']);
      });
    }
  }
}
