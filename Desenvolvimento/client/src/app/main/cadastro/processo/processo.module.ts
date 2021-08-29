import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { SetorService } from './setor.service';
import { ProcessoRoutingModule } from './processo-routing.module';
import { ProcessoListComponent } from './processo-list/processo-list.component';
import { ProcessoDetailComponent } from './processo-detail/processo-detail.component';
import { ProcessoService } from './processo.service';
import { ProcessoSetorGridComponent } from './processo-tabs/setor/setor-grid/setor-grid.component';
import { ProcessoAutorListComponent } from './processo-tabs/autor/autor-list/autor-list.component';
import { ProcessoTramiteListComponent } from './processo-tabs/tramite/tramite-list/tramite-list.component';
import { ProcessoAnexoListComponent } from './processo-tabs/anexo/anexo-list/anexo-list.component';
import { ProcessoModeloListComponent } from './processo-tabs/modelo/modelo-list/modelo-list.component';
import { FuseSharedModule } from '@fuse/shared.module';
import { AssuntoService } from '@fuse/core/assunto.service';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    ProcessoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    ProcessoListComponent,
    ProcessoDetailComponent,
    ProcessoAutorListComponent,
    ProcessoTramiteListComponent,
    ProcessoAnexoListComponent,
    ProcessoModeloListComponent,
    ProcessoSetorGridComponent
  ],
  providers: [
    ProcessoService,
    SetorService,
    AssuntoService
  ],
})
export class ProcessoModule { }
