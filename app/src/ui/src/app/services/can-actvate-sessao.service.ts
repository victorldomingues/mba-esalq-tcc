import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, RouterStateSnapshot } from '@angular/router';
import {Router} from '@angular/router'
@Injectable({
  providedIn: 'root'
})
export class CanActvateSessaoService implements CanActivate {

  constructor( private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): MaybeAsync<GuardResult> {
    const token = localStorage.getItem('accessToken');
    
    if (token) {
      if(this.tokenExpirado(token))
        this.router.navigate(['/login']);
      
      return true;
    }
    
    this.router.navigate(['/login']);
    return false;
  }

  tokenExpirado(token: string): boolean {
    const exp = JSON.parse(atob(token.split('.')[1])).exp;
    return Date.now() >= exp * 1000;
  }
}
