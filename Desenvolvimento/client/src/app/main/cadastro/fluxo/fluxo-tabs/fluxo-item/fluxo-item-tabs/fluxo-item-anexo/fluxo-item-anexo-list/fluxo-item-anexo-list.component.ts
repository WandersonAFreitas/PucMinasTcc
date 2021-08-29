import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoItemAnexoService } from '../fluxo-item-anexo.service';
import { FluxoItemAnexoDetailComponent } from '../fluxo-item-anexo-detail/fluxo-item-anexo-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoItemAnexo } from '@fuse/types/models/fluxo-item-anexo';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-item-anexo-list',
  templateUrl: './fluxo-item-anexo-list.component.html',
  styleUrls: ['./fluxo-item-anexo-list.component.scss']
})
export class FluxoItemAnexoListComponent extends BaseListComponent<FluxoItemAnexo> implements OnInit {

  @Input() fluxoId: number;
  @Input() fluxoItemId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome', 'Obrigatorio', 'ExigeAssinaturaDigital'];
  public entityFilter: FluxoItemAnexo;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoItemAnexo: FluxoItemAnexoService
  ) {
    super(_router, _fluxoItemAnexo, _shellService);

  }

  ngOnInit(): void {
    if (!this.fluxoItemId) {
      this.fluxoItemId = 0;
    }

    this.confFilter();
  }

  confFilter() {
      this.entityFilter = this.initFilter();
      this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
      this.baseLoad(this.gridSettingsModel);
  }

  private initialFilter(): Filter {
    const rules = [ new Rule('fluxoItemId', this.op.eq, '' + this.fluxoItemId) ];
    const initialFilter = new Filter(this.group.and, rules);

    return initialFilter;
  }

  public delete(entity: FluxoItemAnexo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o anexo?`,
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

  private initFilter(): FluxoItemAnexo {
    const model: FluxoItemAnexo = new FluxoItemAnexo(null);
    return model;
  }

  public add(): void {
    const fluxoId = this.fluxoId;
    const fluxoItemId = this.fluxoItemId;

    const dialogRef = this._dialog.open(FluxoItemAnexoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoId,
        fluxoItemId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(fluxoItemAnexo: FluxoItemAnexo): void {
    const fluxoId = this.fluxoId;
    const fluxoItemAnexoId = this.fluxoItemId;

    const dialogRef = this._dialog.open(FluxoItemAnexoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoItemAnexo,
        fluxoId,
        fluxoItemAnexoId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  private afterClosedRefreshGrids(dialogRef: any) {
    this.confFilter();
    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Ok') {
        this.search();
      }
    });
  }
}
