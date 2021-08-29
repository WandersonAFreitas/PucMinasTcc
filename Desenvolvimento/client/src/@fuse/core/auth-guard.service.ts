import { Injectable } from '@angular/core';
import {
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    Router,
    CanActivateChild,
    NavigationExtras
} from '@angular/router';
import { CredentialsService } from './credentials.service';

@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild {

    constructor(private router: Router, private credentialsService: CredentialsService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const url: string = state.url;
        return this.checkLogin(url);
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    checkLogin(url: string): boolean {
        if (this.credentialsService.isLogged()) { return true; }
        this.router.navigate(['/sessions/auth/login']);
        return false;
    }
}
