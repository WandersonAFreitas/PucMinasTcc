import { environment } from './../../environments/environment';
import { CredentialsService } from './credentials.service';
import { ShellService } from './shell.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class ModalLogoffService {

    constructor(
        private router: Router,
        private shellService: ShellService,
        private credentialsService: CredentialsService
    ) { }

    expirarSessao() {

        const config = {
            title: 'Sessão expirada!',
            content: 'O tempo da sessão expirou, efetue o login novamente.',
            isAlert: true,
            alertType: 'warning'
        };

        // this.shellService.confirm().open(config).afterClosed().subscribe(
        //     closeResult => {
        //         if (closeResult) {
        //             this.credentialsService.logout();
        //              this.router.navigate(['/sessions/auth/login']);
        //         }
        //     }
        // );
    }

}
