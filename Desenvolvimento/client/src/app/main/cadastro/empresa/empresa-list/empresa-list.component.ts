import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { EmpresaService } from '../empresa.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Empresa } from '@fuse/types/models/empresa';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-empresa',
  templateUrl: './empresa-list.component.html',
  styleUrls: ['./empresa-list.component.css']
})
export class EmpresaListComponent extends BaseListComponent<Empresa> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Nome', 'Sigla', 'Ativo'];

  public entityFilter: Empresa;

  public statusFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _empresaService: EmpresaService
  ) {
    super(_router, _empresaService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Descricao', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/empresa/new']);
  }

  public edit(entity: Empresa): void {
    this.baseEdit(['/cadastro/empresa/edit', entity.id]);
  }

  public delete(entity: Empresa): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Empresa: ${entity.nome}?`,
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

  private initFilter(): Empresa {
    const model: Empresa = new Empresa(null);
    return model;
  }
}
