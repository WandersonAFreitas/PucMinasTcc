import { CredentialsService } from './credentials.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { navigation } from 'app/navigation/navigation';
// import { NavigationService } from '../shared/services/navigation.service';

@Injectable()
export class RolesGuardService implements CanActivate {
  constructor(
    private router: Router,
    private credentialsService: CredentialsService,
    private _fuseNavigationService: FuseNavigationService
  ) { }


  private spreadItems(myArray: any[]) {
    let newArray = myArray
      .reduce(
        (a, c) => {
          a = [...a, ...c];
          if (c.children) {
            a = [...a, ...this.spreadItems(c.children)];
          }
          return a;
        }, []);
    return newArray;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let menu = this._fuseNavigationService.getNavigation('main');

    if (!menu) {
      if (!this.credentialsService.isLogged()) {
        this.credentialsService.logout();
        return false;
      }
      this._fuseNavigationService.register('main', navigation);
      this._fuseNavigationService.setCurrentNavigation('main');
      menu = this._fuseNavigationService.getNavigation('main');
    }

    const allItems = this.spreadItems(menu);
    const currentUrl: string = state.url;
    const currentUrlSplited = currentUrl.split('/');
    // const mainRoute = currentUrlSplited[2] || currentUrlSplited[1] || currentUrlSplited[0];

    let hasRole = true;
    currentUrlSplited.filter((x: any) => isNaN(x)).filter(x => x != 'edit' && x != 'new' && x != 'list').map(mainRoute => {

      const currentMenuItemByUrl = allItems.filter(x => x.url).find(
        x =>
          `/${x.url}` === mainRoute ||
          x.url === mainRoute ||
          !!~x.url.indexOf(`/${mainRoute}`) ||
          !!~x.url.indexOf(`${mainRoute}/`)
      );

      if (!currentMenuItemByUrl) {
        this.router.navigate(['/sessions/errors/error-404']);
        return false;
      }

      if (currentMenuItemByUrl.roles) {
        hasRole = this.credentialsService.hasSomeRole(currentMenuItemByUrl.roles);
        if (!hasRole) {
          this.router.navigate(['/sessions/errors/error-401']);
          return false;
        }
      }
    });

    return hasRole;
    // const currentMenuItemByUrl = allItems.find(
    //   x =>
    //     `/${x.state}` === currentUrl ||
    //     x.state === currentUrl ||
    //     !!~currentUrl.indexOf(`/${x.state}/`) ||
    //     !!~currentUrl.indexOf(`${x.state}/`)
    // );
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }
}
