import { NgModule } from '@angular/core';
import { LoginModule } from './login/login.module';
import { RegisterModule } from './register/register.module';
import { ForgotPasswordModule } from './forgot-password/forgot-password.module';
import { ResetPasswordModule } from './reset-password/reset-password.module';
import { LockModule } from './lock/lock.module';
import { MailConfirmModule } from './mail-confirm/mail-confirm.module';

@NgModule({
    imports: [
        LoginModule,
        RegisterModule,
        ForgotPasswordModule,
        ResetPasswordModule,
        LockModule,
        MailConfirmModule,
    ]
})
export class AuthenticationModule {

}
