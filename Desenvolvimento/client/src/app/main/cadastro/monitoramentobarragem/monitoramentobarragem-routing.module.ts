import { RouterModule, Routes, } from '@angular/router';
import { NgModule } from '@angular/core';
import { MonitoramentoBarragemListComponent } from './monitoramentobarragem-list/monitoramentobarragem-list.component';
import { MonitoramentoBarragemDetailComponent } from './monitoramentobarragem-detail/monitoramentobarragem-detail.component';
import { RolesGuardService } from '@fuse/core/roles-guard.service';
import { AnalyticsDashboardComponent } from './analytics/analytics.component';
import { AnalyticsDashboardService } from './analytics/analytics.service';


const routes: Routes = [
  {
    path: 'monitoramentobarragem', data: { title: 'MonitoramentoBarragem', breadcrumb: 'MonitoramentoBarragem' },
    canActivateChild: [RolesGuardService],
    children: [      
      { path: '', component: AnalyticsDashboardComponent, resolve: { data: AnalyticsDashboardService } },      
      { path: 'list', component: MonitoramentoBarragemListComponent, data: { title: 'MonitoramentoBarragem', breadcrumb: 'MonitoramentoBarragem' } },
      { path: 'new', component: MonitoramentoBarragemDetailComponent, data: { title: 'Novo', breadcrumb: 'Novo' } },
      { path: 'edit/:id', component: MonitoramentoBarragemDetailComponent, data: { title: 'Editar', breadcrumb: 'Editar' } },
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
export class MonitoramentoBarragemRoutingModule { }
