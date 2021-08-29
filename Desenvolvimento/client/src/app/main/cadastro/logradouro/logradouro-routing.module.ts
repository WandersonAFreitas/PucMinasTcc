import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { LogradouroDetailComponent } from './logradouro-detail/logradouro-detail.component';
import { LogradouroListComponent } from './logradouro-list/logradouro-list.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'logradouro', data: { title: 'Logradouro', breadcrumb: 'Logradouro' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: LogradouroListComponent, data: { title: 'Logradouro', breadcrumb: 'Logradouro' } },
      { path: 'edit/:id', component: LogradouroDetailComponent, data: { title: 'Consulta', breadcrumb: 'Consulta' } },
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
export class LogradouroRoutingModule { }
