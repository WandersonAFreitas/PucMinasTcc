import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { ParametroDetailComponent } from './parametro-detail/parametro-detail.component';
import { ParametroListComponent } from './parametro-list/parametro-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'parametro', data: { title: 'Parametro', breadcrumb: 'Parametro' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: ParametroListComponent, data: { title: 'Parametro', breadcrumb: 'Parametro' } },
      { path: 'new', component: ParametroDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: ParametroDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class ParametroRoutingModule { }
