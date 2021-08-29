import { AcaoRoutingModule } from './acao-routing.module';
import { AcaoListComponent } from './acao-list/acao-list.component';
import { AcaoDetailComponent } from './acao-detail/acao-detail.component';
import { AcaoService } from './acao.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material';
import { FuseSharedModule } from '@fuse/shared.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    AcaoRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    AcaoListComponent,
    AcaoDetailComponent,
  ],
  providers: [
    AcaoService
  ],
})
export class AcaoModule { }
