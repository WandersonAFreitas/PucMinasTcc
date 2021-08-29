import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { MatTabsModule } from '@angular/material';
import { ManageRoutingModule } from './manage-routing.module';
import { ManageService } from './manage.service';
import { ManageUserDetailsComponent } from './user/manage-user-details/manage-user-details.component';
import { ManageUserListComponent } from './user/manage-user-list/manage-user-list.component';
import { ManageRoleListComponent } from './role/manage-role-list/manage-role-list.component';
import { ManageRoleDetailsComponent } from './role/manage-role-details/manage-role-details.component';
import { EmpresaService } from '../cadastro/empresa/empresa.service';
import { ManageUserSetorComponent } from './user/manage-user-setor/manage-user-setor.component';
import { ManageUserRoleComponent } from './user/manage-user-role/manage-user-role.component';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    NgxDatatableModule,
    ChartsModule,
    ManageRoutingModule,
    FuseSharedModule
  ],
  declarations: [
    ManageUserListComponent,
    ManageUserDetailsComponent,
    ManageRoleListComponent,
    ManageRoleDetailsComponent,
    ManageUserSetorComponent,
    ManageUserRoleComponent
  ],
  providers: [
    ManageService,
    EmpresaService
  ],
})
export class ManageModule { }
