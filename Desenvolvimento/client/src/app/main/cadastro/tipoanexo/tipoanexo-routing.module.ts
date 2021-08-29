import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { TipoAnexoListComponent } from './tipoanexo-list/tipoanexo-list.component';
import { TipoAnexoDetailComponent } from './tipoanexo-detail/tipoanexo-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: 'tipoanexo', data: { title: 'TipoAnexo', breadcrumb: 'TipoAnexo' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: TipoAnexoListComponent, data: { title: 'TipoAnexo', breadcrumb: 'TipoAnexo' } },
      { path: 'new', component: TipoAnexoDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: TipoAnexoDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class TipoAnexoRoutingModule { }
