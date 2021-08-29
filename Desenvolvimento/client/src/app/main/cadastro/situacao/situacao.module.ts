import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { SituacaoRoutingModule } from './situacao-routing.module';
import { SituacaoListComponent } from './situacao-list/situacao-list.component';
import { SituacaoDetailComponent } from './situacao-detail/situacao-detail.component';
import { SituacaoService } from './situacao.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    SituacaoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    SituacaoListComponent,
    SituacaoDetailComponent,
  ],
  providers: [
    SituacaoService
  ],
})
export class SituacaoModule { }
