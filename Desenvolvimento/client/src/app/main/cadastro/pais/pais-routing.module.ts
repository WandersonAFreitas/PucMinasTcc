import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { PaisDetailComponent } from './pais-detail/pais-detail.component';
import { PaisListComponent } from './pais-list/pais-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'pais', data: { title: 'Pais', breadcrumb: 'Pais' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: PaisListComponent, data: { title: 'Pais', breadcrumb: 'Pais' } },
      { path: 'new', component: PaisDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: PaisDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class PaisRoutingModule { }
