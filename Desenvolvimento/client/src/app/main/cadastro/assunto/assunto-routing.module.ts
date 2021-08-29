import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { AssuntoDetailComponent } from './assunto-detail/assunto-detail.component';
import { AssuntoListComponent } from './assunto-list/assunto-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'assunto', data: { title: 'Assunto', breadcrumb: 'Assunto' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: AssuntoListComponent, data: { title: 'Assunto', breadcrumb: 'Assunto' } },
      { path: 'new', component: AssuntoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: AssuntoDetailComponent, data: { title: 'Detalhes', breadcrumb: 'Detalhes' } },
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
export class AssuntoRoutingModule { }
