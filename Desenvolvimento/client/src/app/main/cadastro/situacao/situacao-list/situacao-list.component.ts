import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SituacaoService } from '../situacao.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Situacao } from '@fuse/types/models/situacao';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-situacao',
  templateUrl: './situacao-list.component.html',
  styleUrls: ['./situacao-list.component.css']
})
export class SituacaoListComponent extends BaseListComponent<Situacao> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome', 'Padrao'];

  public entityFilter: Situacao;

  public padraoFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  public tipoSituacaoFilter: Array<any> = [
    { key: 1, value: 'Situação inicial' },
    { key: 2, value: 'Próxima situação' },
    { key: 3, value: 'Situação final' },
    { key: 4, value: 'Todas' }
  ];

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _situacaoService: SituacaoService
  ) {
    super(_router, _situacaoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/situacao/new']);
  }

  public edit(entity: Situacao): void {
    this.baseEdit(['/cadastro/situacao/edit', entity.id]);
  }

  public delete(entity: Situacao): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Situacao: ${entity.nome}?`,
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

  private initFilter(): Situacao {
    const model: Situacao = new Situacao(null);
    return model;
  }
}
