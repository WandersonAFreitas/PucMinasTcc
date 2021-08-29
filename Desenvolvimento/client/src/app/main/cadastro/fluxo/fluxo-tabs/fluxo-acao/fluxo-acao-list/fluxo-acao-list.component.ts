import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { FluxoAcaoDetailComponent } from '../fluxo-acao-detail/fluxo-acao-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoAcaoService } from '../fluxo-acao.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoAcao } from '@fuse/types/models/fluxo-acao';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-acao-list',
  templateUrl: './fluxo-acao-list.component.html',
  styleUrls: ['./fluxo-acao-list.component.scss']
})
export class FluxoAcaoListComponent extends BaseListComponent<FluxoAcao> implements OnInit {

  @Input() fluxoId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: FluxoAcao;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoAcaoService: FluxoAcaoService
  ) {
    super(_router, _fluxoAcaoService, _shellService);

    if (!this.fluxoId) {
      this.fluxoId = this.route.params['value'].id || 0;
    }
  }

  ngOnInit(): void {
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
    this.baseLoad(this.gridSettingsModel);
  }

  private initialFilter(): Filter {
    const rules = [new Rule('FluxoId', this.op.eq, '' + this.fluxoId)];
    const initialFilter = new Filter(this.group.and, rules);

    return initialFilter;
  }

  public delete(entity: FluxoAcao): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Ação: ${entity.nome}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Ação removido com sucesso!'], timeout: 3000 });
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

  private initFilter(): FluxoAcao {
    const model: FluxoAcao = new FluxoAcao(null);
    return model;
  }

  public add(): void {
    const fluxoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoAcaoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(fluxoAcao: FluxoAcao): void {
    const fluxoAcaoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoAcaoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoAcao,
        fluxoAcaoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  private afterClosedRefreshGrids(dialogRef: any) {
    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Ok') {
        this.search();
      }
    });
  }

}
