import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { RolesGuardService } from '@fuse/core/roles-guard.service';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: 'home', data: { title: 'Home', breadcrumb: 'Home' },
    canActivateChild: [RolesGuardService],
    children: [
      { path: '', component: HomeComponent, data: { title: 'Home', breadcrumb: 'Home' } }
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
export class HomeRoutingModule { }
