import { NgModule } from '@angular/core';
import { AuthenticationModule } from './authentication/authentication.module';
import { Error404Module } from './errors/404/error-404.module';
import { Error500Module } from './errors/500/error-500.module';
import { Error401Module } from './errors/401/error-401.module';

@NgModule({
  imports: [
    AuthenticationModule,

    // Errors
    Error401Module,
    Error404Module,
    Error500Module,
  ]
})

export class SessionsModule {

}
