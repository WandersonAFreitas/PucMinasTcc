import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FluxoItemService } from '../fluxo-item.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { FluxoItem } from '@fuse/types/models/fluxo-item';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-fluxo-item-list',
  templateUrl: './fluxo-item-list.component.html',
  styleUrls: ['./fluxo-item-list.component.scss']
})
export class FluxoItemListComponent extends BaseListComponent<FluxoItem> implements OnInit {

  @Input() fluxoId: number;
  @Output() tabItem = new EventEmitter();

  public displayedColumns: string[] = ['Actions', 'Id', 'SituacaoAtual', 'Acao', 'ProximaSituacao'];
  public entityFilter: FluxoItem;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoItem: FluxoItemService
  ) {
    super(_router, _fluxoItem, _shellService);
  }

  ngOnInit(): void {

    if (!this.fluxoId) {
      this.fluxoId = 0
    }

    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
    this.baseLoad(this.gridSettingsModel);

  }

  private initialFilter(): Filter {
    const rules = [new Rule('FluxoId', this.op.eq, '' + this.fluxoId)];
    const initialFilter = new Filter(this.group.and, rules);

    return initialFilter;
  }

  public delete(entity: FluxoItem): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o item?`,
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

  public edit(entity: FluxoItem): void {
    this.onTabItem();
    FluxoItemService.fluxoItemId = entity.id;
  }

  onTabItem() {
    this.tabItem.emit(true);
  }

}
