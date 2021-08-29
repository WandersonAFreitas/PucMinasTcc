import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { EmpresaListComponent } from './empresa-list/empresa-list.component';
import { EmpresaDetailComponent } from './empresa-detail/empresa-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'empresa', data: { title: 'Empresa', breadcrumb: 'Empresa' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: EmpresaListComponent, data: { title: 'Empresa', breadcrumb: 'Empresa' } },
      { path: 'new', component: EmpresaDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: EmpresaDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class EmpresaRoutingModule { }
