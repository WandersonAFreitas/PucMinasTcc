import { Injectable } from '@angular/core';
import * as _ from 'lodash';
import { AuthenticatedUser } from '@fuse/types/interfaces/authenticated-user.viewmodel';
import { LocalStorageService } from './local-storage.service';
import { JwtHelper } from '@fuse/utils/jwt-helper';
import { Router } from '@angular/router';

@Injectable()
export class CredentialsService {

    private user: AuthenticatedUser;

    constructor(private router: Router, private localStorageService: LocalStorageService) { }

    getToken() {
        return this.localStorageService.get('token');
    }

    setToken(token: string) {
        this.localStorageService.set('token', token);
    }

    public isLogged() {

        if (!this.getToken()) {
            return false;
        }
        try {

            const jwtHelper = new JwtHelper();
            const decodedToken = jwtHelper.decodeToken(this.getToken());
            if (decodedToken !== null && typeof decodedToken === 'object' && !this.tokenExpired()) {
                this.user = decodedToken.user;
                this.user.roles = decodedToken.roles;
                return true;
            }

            return false;
        } catch (e) {
            return false;
        }
    }

    public get authenticatedUser() {

        if (this.user) {
            return this.user;
        }

        this.isLogged();
        return this.user;
    }

    public tokenExpired() {
        if (this.getToken()) {
            const jwtHelper = new JwtHelper();
            return jwtHelper.isTokenExpired(this.getToken());
        }
        return true;
    }

    public expirationTime(): Date {
        const token = this.getToken();
        const jwtHelper = new JwtHelper();
        return jwtHelper.getTokenExpirationDate(token);
    }

    public logout() {
        this.router.navigate(['/sessions/auth/login']);
        this.localStorageService.remove('token');
    }

    refreshToken(token) {
        this.localStorageService.remove('token');
        this.setToken(token);
    }

    hasRole(role: string) {
        return _.includes(this.user.roles, role);
    }

    hasUniqueRole(role: string) {
        return this.user.roles && this.user.roles.length === 1 && _.includes(this.user.roles, role);
    }

    hasSomeRole(roles: Array<string>): boolean {
        return roles.some((r, i, arr) => this.hasRole(r));
    }

    getUserRoles(): Array<string> {
        return this.user.roles;
    }

    intersectionRoles(roles: Array<string>): Array<string> {
        return roles.filter(role => !!~this.user.roles.indexOf(role));
    }
}
