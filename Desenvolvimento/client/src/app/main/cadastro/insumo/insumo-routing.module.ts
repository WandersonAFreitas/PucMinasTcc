import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { InsumoListComponent } from './insumo-list/insumo-list.component';
import { InsumoDetailComponent } from './insumo-detail/insumo-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'insumo', data: { title: 'Insumo', breadcrumb: 'Insumo' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: InsumoListComponent, data: { title: 'Insumo', breadcrumb: 'Insumo' } },
      { path: 'new', component: InsumoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: InsumoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class InsumoRoutingModule { }
