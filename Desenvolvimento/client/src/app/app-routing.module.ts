import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from '@fuse/core/auth-guard.service';
import { RolesGuardService } from '@fuse/core/roles-guard.service';
import { ResolverService } from './resolver.service';

export const routes: Routes = [
    {
        path: 'home',
        redirectTo: 'cadastro/home',
        pathMatch: 'full'
    },
    {
        path: '',
        canActivate: [AuthGuardService],
        canActivateChild: [RolesGuardService],
        children: [
            {
                path: 'cadastro',
                loadChildren: './main/cadastro/cadastro.module#CadastroModule',
                data: { title: 'Cadastro', breadcrumb: 'Cadastro' }
            },
            {
                path: 'manage',
                loadChildren: './main/manage/manage.module#ManageModule',
                data: { title: 'Administração', breadcrumb: 'Administração' }
            },
            {
                path: 'profile',
                loadChildren: './main/profile/profile.module#ProfileModule',
                data: { title: 'Perfil', breadcrumb: 'Perfil' }
            },
        ]
    },
    {
        path: '',
        children: [
            {
                path: 'sessions',
                loadChildren: './main/sessions/sessions.module#SessionsModule'
            }
        ],
        resolve: {
            someData: ResolverService
          },          
          runGuardsAndResolvers: 'always'
    },
    {
        path: '**',
        redirectTo: 'sessions/errors/error-404'
    }
];
@NgModule({
    imports: [RouterModule.forRoot(routes, {
        useHash: true,
        enableTracing: false,
        onSameUrlNavigation: 'reload'
    }),
    ],
    exports: [RouterModule],
    providers: [
        AuthGuardService,
        RolesGuardService,
        ResolverService
    ]
})
export class AppRoutingModule { }
