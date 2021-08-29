import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FluxoService } from '../fluxo.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Fluxo } from '@fuse/types/models/fluxo';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-fluxo',
  templateUrl: './fluxo-list.component.html',
  styleUrls: ['./fluxo-list.component.css']
})
export class FluxoListComponent extends BaseListComponent<Fluxo> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Descricao', 'Ativo'];

  public entityFilter: Fluxo;

  public ativoFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _fluxoService: FluxoService
  ) {
    super(_router, _fluxoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/fluxo/new']);
  }

  public edit(entity: Fluxo): void {
    this.baseEdit(['/cadastro/fluxo/edit', entity.id]);
  }

  public delete(entity: Fluxo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Fluxo: ${entity.descricao}?`,
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

  private initFilter(): Fluxo {
    const model: Fluxo = new Fluxo(null);
    return model;
  }
}
