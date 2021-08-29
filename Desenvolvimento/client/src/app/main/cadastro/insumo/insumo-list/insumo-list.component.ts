import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { InsumoService } from '../insumo.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Insumo } from '@fuse/types/models/insumo';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-insumo',
  templateUrl: './insumo-list.component.html',
  styleUrls: ['./insumo-list.component.css']
})
export class InsumoListComponent extends BaseListComponent<Insumo> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Identificador', 'Nome', 'Descricao'];

  public entityFilter: Insumo;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _insumoService: InsumoService
  ) {
    super(_router, _insumoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Descricao', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/insumo/new']);
  }

  public edit(entity: Insumo): void {
    this.baseEdit(['/cadastro/insumo/edit', entity.id]);
  }

  public delete(entity: Insumo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Insumo: ${entity.descricao}?`,
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

  private initFilter(): Insumo {
    const model: Insumo = new Insumo(null);
    return model;
  }
}
