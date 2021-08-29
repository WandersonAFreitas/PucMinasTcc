import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { FluxoRoutingModule } from './fluxo-routing.module';
import { FluxoListComponent } from './fluxo-list/fluxo-list.component';
import { FluxoDetailComponent } from './fluxo-detail/fluxo-detail.component';
import { FluxoService } from './fluxo.service';
import { FluxoSituacaoListComponent } from './fluxo-tabs/fluxo-situacao/fluxo-situacao-list/fluxo-situacao-list.component';
import { FluxoSituacaoDetailComponent } from './fluxo-tabs/fluxo-situacao/fluxo-situacao-detail/fluxo-situacao-detail.component';
import { FluxoAcaoListComponent } from './fluxo-tabs/fluxo-acao/fluxo-acao-list/fluxo-acao-list.component';
import { FluxoAcaoDetailComponent } from './fluxo-tabs/fluxo-acao/fluxo-acao-detail/fluxo-acao-detail.component';
import { FluxoTipoAnexoDetailComponent } from './fluxo-tabs/fluxo-tipo-anexo/fluxo-tipo-anexo-detail/fluxo-tipo-anexo-detail.component';
import { FluxoTipoAnexoListComponent } from './fluxo-tabs/fluxo-tipo-anexo/fluxo-tipo-anexo-list/fluxo-tipo-anexo-list.component';
import { FluxoItemListComponent } from './fluxo-tabs/fluxo-item/fluxo-item-list/fluxo-item-list.component';
import { FluxoItemDetailComponent } from './fluxo-tabs/fluxo-item/fluxo-item-detail/fluxo-item-detail.component';
import { FluxoItemChecklistListComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-checklist/fluxo-item-checklist-list/fluxo-item-checklist-list.component';
import { FluxoItemChecklistDetailComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-checklist/fluxo-item-checklist-detail/fluxo-item-checklist-detail.component';
import { FluxoItemAnexoListComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-anexo/fluxo-item-anexo-list/fluxo-item-anexo-list.component';
import { FluxoItemAnexoDetailComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-anexo/fluxo-item-anexo-detail/fluxo-item-anexo-detail.component';
import { FluxoItemSetorListComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-setor/fluxo-item-setor-list/fluxo-item-setor-list.component';
import { FluxoItemSetorDetailComponent } from './fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-setor/fluxo-item-setor-detail/fluxo-item-setor-detail.component';
import { FluxoAssuntoListComponent } from './fluxo-tabs/fluxo-assunto/fluxo-assunto-list/fluxo-assunto-list.component';
import { FluxoAssuntoDetailComponent } from './fluxo-tabs/fluxo-assunto/fluxo-assunto-detail/fluxo-assunto-detail.component';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    FluxoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    FluxoListComponent,
    FluxoDetailComponent,
    FluxoSituacaoListComponent,
    FluxoSituacaoDetailComponent,
    FluxoAcaoListComponent,
    FluxoAcaoDetailComponent,
    FluxoTipoAnexoDetailComponent,
    FluxoTipoAnexoListComponent,
    FluxoItemListComponent,
    FluxoItemDetailComponent,
    FluxoItemChecklistListComponent,
    FluxoItemChecklistDetailComponent,
    FluxoItemAnexoListComponent,
    FluxoItemAnexoDetailComponent,
    FluxoItemSetorListComponent,
    FluxoItemSetorDetailComponent,
    FluxoAssuntoListComponent,
    FluxoAssuntoDetailComponent
  ],
  entryComponents: [
    FluxoSituacaoDetailComponent,
    FluxoAcaoDetailComponent,
    FluxoTipoAnexoDetailComponent,
    FluxoItemChecklistDetailComponent,
    FluxoItemAnexoDetailComponent,
    FluxoItemSetorDetailComponent,
    FluxoAssuntoDetailComponent
  ],
  providers: [
    FluxoService
  ],
})
export class FluxoModule { }
