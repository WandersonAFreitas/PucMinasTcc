import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ParametroService } from '../parametro.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Parametro } from '@fuse/types/models/parametro';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-parametro',
  templateUrl: './parametro-list.component.html',
  styleUrls: ['./parametro-list.component.css']
})
export class ParametroListComponent extends BaseListComponent<Parametro> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Nome'];

  public entityFilter: Parametro;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _parametroService: ParametroService
  ) {
    super(_router, _parametroService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/parametro/new']);
  }

  public edit(entity: Parametro): void {
    this.baseEdit(['/cadastro/parametro/edit', entity.id]);
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): Parametro {
    const model: Parametro = new Parametro(null);
    return model;
  }
}
