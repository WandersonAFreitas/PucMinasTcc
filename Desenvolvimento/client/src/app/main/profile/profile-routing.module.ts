import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { ProfileComponent } from './profile.component';
import { ProfileOverviewComponent } from './profile-overview/profile-overview.component';
import { ProfileSettingsComponent } from './profile-settings/profile-settings.component';
import { ProfileBlankComponent } from './profile-blank/profile-blank.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';

const routes: Routes = [
  {
    path: '', component: ProfileComponent,
    canActivateChild: [RolesGuardService],
    children: [
    // {
    //   path: 'overview',
    //   component: ProfileOverviewComponent,
    //   data: { title: 'Overview', breadcrumb: 'OVERVIEW' }
    // },
    {
      path: 'settings',
      component: ProfileSettingsComponent,
      data: { title: 'Perfil', breadcrumb: 'Perfil' }
    }
    // , {
    //   path: 'blank',
    //   component: ProfileBlankComponent,
    //   data: { title: 'Blank', breadcrumb: 'BLANK' }
    // }
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ProfileRoutingModule { }
