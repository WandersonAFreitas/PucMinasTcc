import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoAssuntoDetailComponent } from '../fluxo-assunto-detail/fluxo-assunto-detail.component';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Assunto } from '@fuse/types/models/assunto';
import { ShellService } from '@fuse/core/shell.service';
import { AssuntoService } from '@fuse/core/assunto.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-assunto-list',
  templateUrl: './fluxo-assunto-list.component.html',
  styleUrls: ['./fluxo-assunto-list.component.scss']
})
export class FluxoAssuntoListComponent extends BaseListComponent<Assunto> implements OnInit {

  @Input() fluxoId: number;
  @Output() dataUserSetor = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'Empresa', 'Nome'];

  public entityFilter: Assunto;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _assuntoService: AssuntoService
  ) {
    super(_router, _assuntoService, _shellService);

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

  public delete(entity: Assunto): void {
    this.baseDelete({
      titulo: `Confirmar desvinculação`,
      mensagem: `Deseja realmente desvincular assunto: ${entity.nome}?`,
      callback: () => {
        this._assuntoService.putDesvincularFluxo(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Assunto desvinculado com sucesso!'], timeout: 3000 });
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

  private initFilter(): Assunto {
    const model: Assunto = new Assunto(null);
    return model;
  }

  public add(): void {

    const fluxoId = this.fluxoId;

    const dialogRef = this._dialog.open(FluxoAssuntoDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        fluxoId
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
