import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoItemChecklistService } from '../fluxo-item-checklist.service';
import { FluxoItemChecklistDetailComponent } from '../fluxo-item-checklist-detail/fluxo-item-checklist-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoItemCheckList } from '@fuse/types/models/fluxo-item-checklist';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-item-checklist-list',
  templateUrl: './fluxo-item-checklist-list.component.html',
  styleUrls: ['./fluxo-item-checklist-list.component.scss']
})
export class FluxoItemChecklistListComponent extends BaseListComponent<FluxoItemCheckList> implements OnInit {

  @Input() fluxoItemId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];
  public entityFilter: FluxoItemCheckList;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoItemChecklist: FluxoItemChecklistService
  ) {
    super(_router, _fluxoItemChecklist, _shellService);

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

  public delete(entity: FluxoItemCheckList): void {
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

  private initFilter(): FluxoItemCheckList {
    const model: FluxoItemCheckList = new FluxoItemCheckList(null);
    return model;
  }

  public add(): void {
    const fluxoItemId = this.fluxoItemId;
    const dialogRef = this._dialog.open(FluxoItemChecklistDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoItemId
      },
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(fluxoItemCheckList: FluxoItemCheckList): void {
    const fluxoItemCheckListId = this.fluxoItemId;
    const dialogRef = this._dialog.open(FluxoItemChecklistDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoItemCheckList,
        fluxoItemCheckListId
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
