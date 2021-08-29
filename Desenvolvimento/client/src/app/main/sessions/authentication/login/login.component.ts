import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { SigninViewModel } from '@fuse/types/interfaces/signin-viewmodel';
import { MatButton } from '@angular/material';
import { CredentialsService } from '@fuse/core/credentials.service';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { FuseNavigation } from '@fuse/types';
import { navigation } from 'app/navigation/navigation';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {

    @ViewChild('submitButton') submitButton: MatButton;

    loginForm: FormGroup;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _signinService: LoginService,
        private _fuseNavigationService: FuseNavigationService,
        private _credentialsService: CredentialsService,
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _router: Router
    ) {
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
                },
                footer: {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    signin() {
        const signinData: SigninViewModel = this.loginForm.value

        this.submitButton.disabled = true;
        // this.progressBar.mode = 'indeterminate';

        this._signinService.requestToken(signinData).subscribe(res => {
            this._credentialsService.setToken(res.body.token);
            setTimeout(() => {
                this._router.navigate(['home']);
                // Register the navigation to the service
                this._fuseNavigationService.register('main', navigation);
                // Set the main navigation as our current navigation
                this._fuseNavigationService.setCurrentNavigation('main');
            }, 2000);
        },
            err => {
                // this.progressBar.mode = null;
                // this._shellService.alert().warning(
                //     {
                //         timeout: 2000,
                //         messages: err.error ? err.error.userMessages : ['Login ou Password inválido']
                //     });
                this.loginForm.get('password').setValue('');
            });
    }
}
