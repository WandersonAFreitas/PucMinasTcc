import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { ManageUserListComponent } from './user/manage-user-list/manage-user-list.component';
import { ManageUserDetailsComponent } from './user/manage-user-details/manage-user-details.component';
import { ManageRoleListComponent } from './role/manage-role-list/manage-role-list.component';
import { ManageRoleDetailsComponent } from './role/manage-role-details/manage-role-details.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'user', data: { title: 'Usuário', breadcrumb: 'Usuário' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: ManageUserListComponent, data: { title: 'Listar', breadcrumb: 'Listar' } },
      { path: 'new', component: ManageUserDetailsComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: ManageUserDetailsComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
    ]
  },
  {
    path: 'role', data: { title: 'Papeis', breadcrumb: 'Papeis' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: ManageRoleListComponent, data: { title: 'Listar', breadcrumb: 'Listar' } },
      { path: 'new', component: ManageRoleDetailsComponent, data: { title: 'Novo', breadcrumb: 'New' } },
      { path: 'edit/:id', component: ManageRoleDetailsComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ManageRoutingModule { }
