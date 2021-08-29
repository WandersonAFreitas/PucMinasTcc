import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { ProcessoListComponent } from './processo-list/processo-list.component';
import { ProcessoDetailComponent } from './processo-detail/processo-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'processo/todos',
    pathMatch: 'full'
  },
  {
    path: 'processo', data: { title: 'Processo', breadcrumb: 'Processo' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: 'todos', component: ProcessoListComponent, data: { title: 'Processo', breadcrumb: 'Processo' } },
      { path: 'meusprocessos', component: ProcessoListComponent, data: { title: 'Processo', breadcrumb: 'Processo' } },
      { path: 'naoatribuido', component: ProcessoListComponent, data: { title: 'Processo', breadcrumb: 'Processo' } },
      { path: 'new', component: ProcessoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: ProcessoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class ProcessoRoutingModule { }
