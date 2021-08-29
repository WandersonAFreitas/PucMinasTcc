import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { LogradouroRoutingModule } from './logradouro-routing.module';
import { LogradouroListComponent } from './logradouro-list/logradouro-list.component';
import { LogradouroDetailComponent } from './logradouro-detail/logradouro-detail.component';
import { LogradouroService } from './logradouro.service';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    LogradouroRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    LogradouroListComponent,
    LogradouroDetailComponent,
  ],
  providers: [
    LogradouroService
  ],
})
export class LogradouroModule { }
