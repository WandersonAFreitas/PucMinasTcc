import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MonitoramentoBarragemService } from '../monitoramentobarragem.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { MonitoramentoBarragem } from '@fuse/types/models/monitoramento-barragem';
import { ShellService } from '@fuse/core/shell.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-monitoramentobarragem',
  templateUrl: './monitoramentobarragem-list.component.html',
  styleUrls: ['./monitoramentobarragem-list.component.css']
})
export class MonitoramentoBarragemListComponent extends BaseListComponent<MonitoramentoBarragem> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Descricao', 'DataHora'];

  public entityFilter: MonitoramentoBarragem;

  constructor(
    private _router: Router,
    private _location: Location,
    private _shellService: ShellService,
    private _monitoramentobarragemService: MonitoramentoBarragemService
  ) {
    super(_router, _monitoramentobarragemService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Descricao', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/monitoramentobarragem/new']);
  }

  public edit(entity: MonitoramentoBarragem): void {
    this.baseEdit(['/cadastro/monitoramentobarragem/edit', entity.id]);
  }

  public delete(entity: MonitoramentoBarragem): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o MonitoramentoBarragem: ${entity.descricao}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
        });
      }
    });
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): MonitoramentoBarragem {
    const model: MonitoramentoBarragem = new MonitoramentoBarragem(null);
    return model;
  }

  public goBack(): void {
    this._location.back();
  }
}
