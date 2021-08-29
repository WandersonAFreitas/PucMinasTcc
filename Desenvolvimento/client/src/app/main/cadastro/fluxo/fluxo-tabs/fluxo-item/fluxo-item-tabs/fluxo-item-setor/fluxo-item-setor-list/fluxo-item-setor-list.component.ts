import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoItemSetorService } from '../../fluxo-item-setor.service';
import { FluxoItemSetorDetailComponent } from '../fluxo-item-setor-detail/fluxo-item-setor-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoItemSetor } from '@fuse/types/models/fluxo-item-setor';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-item-setor-list',
  templateUrl: './fluxo-item-setor-list.component.html',
  styleUrls: ['./fluxo-item-setor-list.component.scss']
})
export class FluxoItemSetorListComponent extends BaseListComponent<FluxoItemSetor> implements OnInit {

  @Input() fluxoId: number;
  @Input() fluxoItemId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Empresa', 'Nome'];
  public entityFilter: FluxoItemSetor;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoItemSetor: FluxoItemSetorService
  ) {
    super(_router, _fluxoItemSetor, _shellService);

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
    const rules = [new Rule('fluxoItemId', this.op.eq, '' + this.fluxoItemId)];
    const initialFilter = new Filter(this.group.and, rules);

    return initialFilter;
  }

  public delete(entity: FluxoItemSetor): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o setor?`,
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

  private initFilter(): FluxoItemSetor {
    const model: FluxoItemSetor = new FluxoItemSetor(null);
    return model;
  }

  public add(): void {
    const fluxoId = this.fluxoId;
    const fluxoItemId = this.fluxoItemId;

    const dialogRef = this._dialog.open(FluxoItemSetorDetailComponent, {
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

  public edit(fluxoItemSetor: FluxoItemSetor): void {
    const fluxoId = this.fluxoId;
    const fluxoItemSetorId = this.fluxoItemId;

    const dialogRef = this._dialog.open(FluxoItemSetorDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoItemSetor,
        fluxoId,
        fluxoItemSetorId
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
