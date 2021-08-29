import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { SituacaoDetailComponent } from './situacao-detail/situacao-detail.component';
import { SituacaoListComponent } from './situacao-list/situacao-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'situacao', data: { title: 'Situacao', breadcrumb: 'Situacao' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: SituacaoListComponent, data: { title: 'Situacao', breadcrumb: 'Situacao' } },
      { path: 'new', component: SituacaoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: SituacaoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class SituacaoRoutingModule { }
