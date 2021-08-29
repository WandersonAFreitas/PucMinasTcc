import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule, MatButtonModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatSelectModule } from '@angular/material';
import { MonitoramentoBarragemRoutingModule } from './monitoramentobarragem-routing.module';
import { MonitoramentoBarragemListComponent } from './monitoramentobarragem-list/monitoramentobarragem-list.component';
import { MonitoramentoBarragemDetailComponent } from './monitoramentobarragem-detail/monitoramentobarragem-detail.component';
import { MonitoramentoBarragemService } from './monitoramentobarragem.service';
import { AgmCoreModule } from '@agm/core';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components/widget/widget.module';

import { AnalyticsDashboardComponent } from './analytics/analytics.component';
import { AnalyticsDashboardService } from './analytics/analytics.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    MonitoramentoBarragemRoutingModule,
    FuseSharedModule,
    
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatMenuModule,
    MatSelectModule,
    MatTabsModule,

    AgmCoreModule.forRoot({
            apiKey: 'AIzaSyD81ecsCj4yYpcXSLFcYU97PvRsE_X8Bx8'
        }),
    ChartsModule,
    NgxChartsModule,

    FuseSharedModule,
    FuseWidgetModule
  ],
  declarations: [
    MonitoramentoBarragemListComponent,
    MonitoramentoBarragemDetailComponent,
    AnalyticsDashboardComponent
  ],
  providers: [
    MonitoramentoBarragemService,
    AnalyticsDashboardService
  ],
})
export class MonitoramentoBarragemModule { }
