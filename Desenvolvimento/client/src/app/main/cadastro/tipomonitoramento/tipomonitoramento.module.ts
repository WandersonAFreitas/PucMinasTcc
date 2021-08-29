import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';
import { TipoMonitoramentoService } from './tipomonitoramento.service';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    FuseSharedModule
  ],
  declarations: [

  ],
  providers: [
    TipoMonitoramentoService
  ],
})
export class TipoMonitoramentomentoModule { }
