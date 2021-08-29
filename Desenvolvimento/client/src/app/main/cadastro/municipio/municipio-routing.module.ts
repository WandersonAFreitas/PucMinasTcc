import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { MunicipioDetailComponent } from './municipio-detail/municipio-detail.component';
import { MunicipioListComponent } from './municipio-list/municipio-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'municipio', data: { title: 'Municipio', breadcrumb: 'Municipio' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: MunicipioListComponent, data: { title: 'Municipio', breadcrumb: 'Municipio' } },
      { path: 'new', component: MunicipioDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: MunicipioDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class MunicipioRoutingModule { }
