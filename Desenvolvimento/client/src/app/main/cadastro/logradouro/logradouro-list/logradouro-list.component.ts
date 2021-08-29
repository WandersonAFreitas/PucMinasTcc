import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { LogradouroService } from '../logradouro.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Logradouro } from '@fuse/types/models/logradouro';
import { ShellService } from '@fuse/core/shell.service';
import { Pais } from '@fuse/types/models/pais';
import { Estado } from '@fuse/types/models/estado';
import { Municipio } from '@fuse/types/models/municipio';

@Component({
  selector: 'app-logradouro',
  templateUrl: './logradouro-list.component.html',
  styleUrls: ['./logradouro-list.component.css']
})
export class LogradouroListComponent extends BaseListComponent<Logradouro> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'CEP', 'Endereco', 'Bairro', 'Pais.Nome', 'Estado.Nome', 'Municipio.Nome'];

  public entityFilter: Logradouro;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _logradouroService: LogradouroService
  ) {
    super(_router, _logradouroService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Id', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/logradouro/new']);
  }

  public edit(entity: Logradouro): void {
    this.baseEdit(['/cadastro/logradouro/edit', entity.id]);
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): Logradouro {
    const model: Logradouro = new Logradouro(null);
    model.pais = new Pais(null);
    model.estado = new Estado(null);
    model.municipio = new Municipio(null);
    return model;
  }
}
