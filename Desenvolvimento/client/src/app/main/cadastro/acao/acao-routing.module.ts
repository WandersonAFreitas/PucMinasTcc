import { AcaoDetailComponent } from './acao-detail/acao-detail.component';
import { AcaoListComponent } from './acao-list/acao-list.component';
import { Routes, RouterModule } from '@angular/router';
import { RolesGuardService } from '@fuse/core/roles-guard.service';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: 'acao', data: { title: 'Acao', breadcrumb: 'Acao' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: AcaoListComponent, data: { title: 'Acao', breadcrumb: 'Acao' } },
      { path: 'new', component: AcaoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: AcaoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class AcaoRoutingModule { }
