import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AssuntoService } from '../assunto.service';
import { SetorService } from '../../empresa/setor.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Assunto } from '@fuse/types/models/assunto';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { Empresa } from '@fuse/types/models/empresa';

@Component({
  selector: 'app-assunto',
  templateUrl: './assunto-list.component.html',
  styleUrls: ['./assunto-list.component.css']
})
export class AssuntoListComponent extends BaseListComponent<Assunto> implements OnInit {

  public statusFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  public displayedColumns: string[] = [
    'Actions',
    'Nome',
    'Empresa.Nome',
    // TODO: REMOVER
    // 'SetorProcessoFisico.Sigla',
    // 'SetorProcessoVirtual.Sigla',
    'Ativo'];

  public entityFilter: Assunto;

  public gridSettingsModelToFlterAutocomplete: GridSettings;

  constructor(
    private _router: Router,
    public _setorService: SetorService,
    private _shellService: ShellService,
    private _assuntoService: AssuntoService
  ) {
    super(_router, _assuntoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
    this.initGridSettingsModel();
  }

  private initGridSettingsModel() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModelToFlterAutocomplete = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  // public create(): void {
  //   this.router.navigate(['/cadastro/assunto/new']);
  // }

  public edit(entity: Assunto): void {
    this.baseEdit(['/cadastro/assunto/edit', entity.id]);
  }

  // public delete(entity: Assunto): void {
  //   this.baseDelete({
  //     titulo: `Confirmar Exclusão`,
  //     mensagem: `Deseja realmente excluir o Assunto: ${entity.nome}?`,
  //     callback: () => {
  //       this.service.remove(entity.id).subscribe(() => {
  //         if (this.pagination.content.length === 1) {
  //           this.baseSetToFistPageGridSettings();
  //         }
  //         this.baseSearch(this.gridSettingsModel);
  //       });
  //     }
  //   });
  // }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear();
  }

  private initFilter(): Assunto {
    const model: Assunto = new Assunto(null);
    model.empresa = new Empresa(null);
    return model;
  }
}
