import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { ParametroRoutingModule } from './parametro-routing.module';
import { ParametroListComponent } from './parametro-list/parametro-list.component';
import { ParametroDetailComponent } from './parametro-detail/parametro-detail.component';
import { ParametroService } from './parametro.service';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    ChartsModule,
    ParametroRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    ParametroListComponent,
    ParametroDetailComponent,
  ],
  providers: [
    ParametroService
  ],
})
export class ParametroModule { }
