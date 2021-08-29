import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { EstadoDetailComponent } from './estado-detail/estado-detail.component';
import { EstadoListComponent } from './estado-list/estado-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'estado', data: { title: 'Estado', breadcrumb: 'Estado' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: EstadoListComponent, data: { title: 'Estado', breadcrumb: 'Estado' } },
      { path: 'new', component: EstadoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: EstadoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class EstadoRoutingModule { }
