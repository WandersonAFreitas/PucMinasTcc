import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { ProfileComponent } from './profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileOverviewComponent } from './profile-overview/profile-overview.component';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { ProfileBlankComponent } from './profile-blank/profile-blank.component';
import { ManageService } from '../manage/manage.service';
import { FuseSharedModule } from '@fuse/shared.module';
import { MaterialModule } from '@fuse/material.module';

@NgModule({
  imports: [
    MaterialModule,
    NgxDatatableModule,
    CommonModule,
    FormsModule,
    FuseSharedModule,
    ProfileRoutingModule
  ],
  declarations: [ProfileComponent, ProfileOverviewComponent, ProfileSettingsComponent, ProfileBlankComponent],
  providers: [
    ManageService
  ],
})
export class ProfileModule { }
