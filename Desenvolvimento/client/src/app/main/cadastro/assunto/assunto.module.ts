import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { AssuntoRoutingModule } from './assunto-routing.module';
import { AssuntoListComponent } from './assunto-list/assunto-list.component';
import { AssuntoDetailComponent } from './assunto-detail/assunto-detail.component';
import { AssuntoService } from './assunto.service';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    ChartsModule,
    AssuntoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    AssuntoListComponent,
    AssuntoDetailComponent,
  ],
  providers: [
    AssuntoService
  ],
})
export class AssuntoModule { }
