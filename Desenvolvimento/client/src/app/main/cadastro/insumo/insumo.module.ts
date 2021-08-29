import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { InsumoRoutingModule } from './insumo-routing.module';
import { InsumoListComponent } from './insumo-list/insumo-list.component';
import { InsumoDetailComponent } from './insumo-detail/insumo-detail.component';
import { InsumoService } from './insumo.service';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    InsumoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    InsumoListComponent,
    InsumoDetailComponent,
  ],
  providers: [
    InsumoService
  ],
})
export class InsumoModule { }
