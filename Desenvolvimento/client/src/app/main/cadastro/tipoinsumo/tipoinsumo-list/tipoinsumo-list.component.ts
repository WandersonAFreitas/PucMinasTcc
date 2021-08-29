import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { TipoInsumoService } from '../tipoinsumo.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { TipoInsumo } from '@fuse/types/models/tipo-insumo';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-tipoinsumo',
  templateUrl: './tipoinsumo-list.component.html',
  styleUrls: ['./tipoinsumo-list.component.css']
})
export class TipoInsumoListComponent extends BaseListComponent<TipoInsumo> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Descricao'];

  public entityFilter: TipoInsumo;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _tipoinsumoService: TipoInsumoService
  ) {
    super(_router, _tipoinsumoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Descricao', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/tipoinsumo/new']);
  }

  public edit(entity: TipoInsumo): void {
    this.baseEdit(['/cadastro/tipoinsumo/edit', entity.id]);
  }

  public delete(entity: TipoInsumo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o TipoInsumo: ${entity.descricao}?`,
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

  private initFilter(): TipoInsumo {
    const model: TipoInsumo = new TipoInsumo(null);
    return model;
  }
}
