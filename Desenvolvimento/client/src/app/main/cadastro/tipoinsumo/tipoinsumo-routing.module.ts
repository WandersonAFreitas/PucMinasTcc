import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { TipoInsumoListComponent } from './tipoinsumo-list/tipoinsumo-list.component';
import { TipoInsumoDetailComponent } from './tipoinsumo-detail/tipoinsumo-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'tipoinsumo', data: { title: 'TipoInsumo', breadcrumb: 'TipoInsumo' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: TipoInsumoListComponent, data: { title: 'TipoInsumo', breadcrumb: 'TipoInsumo' } },
      { path: 'new', component: TipoInsumoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: TipoInsumoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class TipoInsumoRoutingModule { }
