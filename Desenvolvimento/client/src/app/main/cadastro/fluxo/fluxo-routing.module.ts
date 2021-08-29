import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { FluxoDetailComponent } from './fluxo-detail/fluxo-detail.component';
import { FluxoListComponent } from './fluxo-list/fluxo-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'fluxo', data: { title: 'Fluxo', breadcrumb: 'Fluxo' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: FluxoListComponent, data: { title: 'Fluxo', breadcrumb: 'Fluxo' } },
      { path: 'new', component: FluxoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: FluxoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class FluxoRoutingModule { }
