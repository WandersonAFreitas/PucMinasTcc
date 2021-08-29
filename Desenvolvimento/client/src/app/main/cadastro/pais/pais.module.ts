import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { PaisRoutingModule } from './pais-routing.module';
import { PaisListComponent } from './pais-list/pais-list.component';
import { PaisDetailComponent } from './pais-detail/pais-detail.component';
import { PaisService } from './pais.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    PaisRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    PaisListComponent,
    PaisDetailComponent,
  ],
  providers: [
    PaisService
  ],
})
export class PaisModule { }
