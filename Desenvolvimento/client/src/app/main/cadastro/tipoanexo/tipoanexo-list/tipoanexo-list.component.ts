import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { TipoAnexoService } from '../tipoanexo.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { TipoAnexo } from '@fuse/types/models/tipo-anexo';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-tipoanexo',
  templateUrl: './tipoanexo-list.component.html',
  styleUrls: ['./tipoanexo-list.component.css']
})
export class TipoAnexoListComponent extends BaseListComponent<TipoAnexo> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: TipoAnexo;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _tipoanexoService: TipoAnexoService
  ) {
    super(_router, _tipoanexoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/tipoanexo/new']);
  }

  public edit(entity: TipoAnexo): void {
    this.baseEdit(['/cadastro/tipoanexo/edit', entity.id]);
  }

  public delete(entity: TipoAnexo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o TipoAnexo: ${entity.nome}?`,
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

  private initFilter(): TipoAnexo {
    const model: TipoAnexo = new TipoAnexo(null);
    return model;
  }
}
