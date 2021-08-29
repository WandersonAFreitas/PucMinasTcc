import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { TipoAnexoRoutingModule } from './tipoanexo-routing.module';
import { TipoAnexoListComponent } from './tipoanexo-list/tipoanexo-list.component';
import { TipoAnexoDetailComponent } from './tipoanexo-detail/tipoanexo-detail.component';
import { TipoAnexoService } from './tipoanexo.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    TipoAnexoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    TipoAnexoListComponent,
    TipoAnexoDetailComponent,
  ],
  providers: [
    TipoAnexoService
  ],
})
export class TipoAnexoModule { }
