import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';
import { Error401Component } from './error-401.component';


const routes = [
    {
        path     : 'errors/error-401',
        component: Error401Component
    }
];

@NgModule({
    declarations: [
        Error401Component
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatIconModule,

        FuseSharedModule
    ]
})
export class Error401Module
{
}
