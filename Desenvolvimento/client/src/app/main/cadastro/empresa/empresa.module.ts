import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material';
import { EmpresaRoutingModule } from './empresa-routing.module';
import { EmpresaListComponent } from './empresa-list/empresa-list.component';
import { EmpresaService } from './empresa.service';
import { EmpresaDetailComponent } from './empresa-detail/empresa-detail.component';
import { SetorService } from './setor.service';
import { FuseSharedModule } from '@fuse/shared.module';
import { EmpresaAssuntoListComponent } from './empresa-tabs/assunto/assunto-list/assunto-list.component';
import { EmpresaSetorGridComponent } from './empresa-tabs/setor/setor-grid/setor-grid.component';
import { AssuntoService } from '@fuse/core/assunto.service';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    EmpresaRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    EmpresaListComponent,
    EmpresaDetailComponent,
    EmpresaAssuntoListComponent,
    EmpresaSetorGridComponent,
  ],
  providers: [
    EmpresaService,
    SetorService,
    AssuntoService,
  ],
})
export class EmpresaModule { }