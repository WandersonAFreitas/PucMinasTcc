import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { HomeRoutingModule } from './home-routing.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { HomeComponent } from './home/home.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    HomeRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    HomeComponent
  ],
  providers: [
  ],
})
export class HomeModule { }
