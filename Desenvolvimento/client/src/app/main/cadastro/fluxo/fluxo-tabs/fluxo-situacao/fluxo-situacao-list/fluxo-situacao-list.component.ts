import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoSituacaoService } from '../fluxo-situacao.service';
import { FluxoSituacaoDetailComponent } from '../fluxo-situacao-detail/fluxo-situacao-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoSituacao } from '@fuse/types/models/fluxo-situacao';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-situacao-list',
  templateUrl: './fluxo-situacao-list.component.html',
  styleUrls: ['./fluxo-situacao-list.component.scss']
})
export class FluxoSituacaoListComponent extends BaseListComponent<FluxoSituacao> implements OnInit {

  @Input() fluxoId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: FluxoSituacao;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoSituacaoService: FluxoSituacaoService
  ) {
    super(_router, _fluxoSituacaoService, _shellService);

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

  public delete(entity: FluxoSituacao): void {
    if (entity.padrao) {
      this._shellService.alert().info({ messages: ['Situacao padrão não pode ser excluída!'], timeout: 3000 });
      return;
    }

    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Situacao: ${entity.nome}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Situacao removido com sucesso!'], timeout: 3000 });
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

  private initFilter(): FluxoSituacao {
    const model: FluxoSituacao = new FluxoSituacao(null);
    return model;
  }

  public add(): void {
    const fluxoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoSituacaoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(fluxoSituacao: FluxoSituacao): void {
    if (fluxoSituacao.padrao) {
      this._shellService.alert().info({ messages: ['Situacao padrão não pode ser alterada!'], timeout: 3000 });
      return;
    }

    const fluxoSituacaoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoSituacaoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoSituacao,
        fluxoSituacaoId
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
