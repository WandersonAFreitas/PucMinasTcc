import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { EstadoRoutingModule } from './estado-routing.module';
import { EstadoListComponent } from './estado-list/estado-list.component';
import { EstadoDetailComponent } from './estado-detail/estado-detail.component';
import { EstadoService } from './estado.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    EstadoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    EstadoListComponent,
    EstadoDetailComponent,
  ],
  providers: [
    EstadoService
  ],
})
export class EstadoModule { }
