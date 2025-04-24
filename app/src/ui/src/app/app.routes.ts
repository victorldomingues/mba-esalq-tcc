import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { MovimentacoesComponent } from './pages/movimentacoes/movimentacoes.component';
import { CadastroComponent } from './pages/cadastro/cadastro.component';
import { NovaMovimentacaoComponent } from './pages/nova-movimentacao/nova-movimentacao.component';
import { CanActvateSessaoService } from './services/can-actvate-sessao.service';

export const routes: Routes = [
    {path: '', component: HomeComponent, canActivate: [CanActvateSessaoService], children:[

    ]},
    {path: 'movimentacoes', component: MovimentacoesComponent, canActivate: [CanActvateSessaoService]},
    {path: 'nova-movimentacao', component: NovaMovimentacaoComponent,canActivate: [CanActvateSessaoService]},
    {path: 'login', component: LoginComponent},
    {path: 'cadastro', component: CadastroComponent},
    {path: '**', redirectTo: ''}
];
