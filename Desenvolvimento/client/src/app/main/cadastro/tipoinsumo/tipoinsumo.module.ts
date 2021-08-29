import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { TipoInsumoRoutingModule } from './tipoinsumo-routing.module';
import { TipoInsumoListComponent } from './tipoinsumo-list/tipoinsumo-list.component';
import { TipoInsumoDetailComponent } from './tipoinsumo-detail/tipoinsumo-detail.component';
import { TipoInsumoService } from './tipoinsumo.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    TipoInsumoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    TipoInsumoListComponent,
    TipoInsumoDetailComponent,
  ],
  providers: [
    TipoInsumoService
  ],
})
export class TipoInsumoModule { }
