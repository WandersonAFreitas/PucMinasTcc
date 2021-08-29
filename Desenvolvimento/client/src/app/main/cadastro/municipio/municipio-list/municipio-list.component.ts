import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { MunicipioService } from '../municipio.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Municipio } from '@fuse/types/models/municipio';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-municipio',
  templateUrl: './municipio-list.component.html',
  styleUrls: ['./municipio-list.component.css']
})
export class MunicipioListComponent extends BaseListComponent<Municipio> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome', 'Estado'];

  public entityFilter: Municipio;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _municipioService: MunicipioService
  ) {
    super(_router, _municipioService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Id', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/municipio/new']);
  }

  public edit(entity: Municipio): void {
    this.baseEdit(['/cadastro/municipio/edit', entity.id]);
  }

  public delete(entity: Municipio): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Municipio: ${entity.nome}?`,
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

  private initFilter(): Municipio {
    const model: Municipio = new Municipio(null);
    return model;
  }
}
