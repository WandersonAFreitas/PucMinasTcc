import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoTipoAnexoService } from '../fluxo-tipo-anexo.service';
import { FluxoTipoAnexoDetailComponent } from '../fluxo-tipo-anexo-detail/fluxo-tipo-anexo-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoTipoAnexo } from '@fuse/types/models/fluxo-tipo-anexo';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-tipo-anexo-list',
  templateUrl: './fluxo-tipo-anexo-list.component.html',
  styleUrls: ['./fluxo-tipo-anexo-list.component.scss']
})
export class FluxoTipoAnexoListComponent extends BaseListComponent<FluxoTipoAnexo> implements OnInit {

  @Input() fluxoId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: FluxoTipoAnexo;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoTipoAnexoService: FluxoTipoAnexoService
  ) {
    super(_router, _fluxoTipoAnexoService, _shellService);

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
    const rules = [ new Rule('FluxoId', this.op.eq, '' + this.fluxoId) ];
    const initialFilter = new Filter(this.group.and, rules);

    return initialFilter;
  }

  public delete(entity: FluxoTipoAnexo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Tipo de Anexo: ${entity.nome}?`,
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

  private initFilter(): FluxoTipoAnexo {
    const model: FluxoTipoAnexo = new FluxoTipoAnexo(null);
    return model;
  }

  public add(): void {
    const fluxoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoTipoAnexoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(fluxoTipoAnexo: FluxoTipoAnexo): void {
    const fluxoSituacaoId = this.fluxoId;
    const dialogRef = this._dialog.open(FluxoTipoAnexoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoTipoAnexo,
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
