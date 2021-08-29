import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { MunicipioRoutingModule } from './municipio-routing.module';
import { MunicipioListComponent } from './municipio-list/municipio-list.component';
import { MunicipioDetailComponent } from './municipio-detail/municipio-detail.component';
import { MunicipioService } from './municipio.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    MunicipioRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    MunicipioListComponent,
    MunicipioDetailComponent,
  ],
  providers: [
    MunicipioService
  ],
})
export class MunicipioModule { }
