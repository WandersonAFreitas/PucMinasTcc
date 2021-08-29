import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { SetorService } from '../../../setor.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { TramiteArquivo } from '@fuse/types/models/tramite-arquivo';
import { Processo } from '@fuse/types/models/processo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { FluxoAcaoService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-acao/fluxo-acao.service';
import { ArquivoService } from '@fuse/core/arquivo.service';
import { ShellService } from '@fuse/core/shell.service';
import { TramiteArquivoService } from '@fuse/core/tramite-arquivo.service';

@Component({
  selector: 'processo-tab-anexos',
  templateUrl: './anexo-list.component.html',
  styleUrls: ['./anexo-list.component.css']
})
export class ProcessoAnexoListComponent extends BaseListComponent<TramiteArquivo> implements OnInit {

  @Input() public processo: Processo;

  public processoId: number;
  public displayedColumns: string[] = ['Arquivo.Nome', 'Tramite.Acao.Nome', 'Tramite.Setor.Nome', 'Actions'];

  public entityFilter: TramiteArquivo;

  public gridSettingsModelToFilterAutocompleteSetor: GridSettings;
  public gridSettingsModelToFilterAutocompleteAcao: GridSettings;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    public _setorService: SetorService,
    public _acaoService: FluxoAcaoService,
    private _arquivoService: ArquivoService,
    private _shellService: ShellService,
    private _tramiteArquivoService: TramiteArquivoService
  ) {
    super(_router, _tramiteArquivoService, _shellService);
    this.processoId = this.route.params['value'].id || 0;
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
    this.initGridSettingsModelSetor();
    this.initGridSettingsModelAcao();
  }

  private initGridSettingsModelSetor() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModelToFilterAutocompleteSetor = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  private initGridSettingsModelAcao() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModelToFilterAutocompleteAcao = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }


  private initialFilter(): Filter {
    const rules = [
      new Rule('Tramite.ProcessoId', this.op.eq, '' + this.processoId),
    ];

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public delete(entity: TramiteArquivo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Anexo?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Anexo removido com sucesso!'], timeout: 3000 });
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
    this.baseClear(this.initialFilter());
  }

  private initFilter(): TramiteArquivo {
    const model: TramiteArquivo = new TramiteArquivo(null);
    return model;
  }

  public downloadArquivo(tramiteArquivo: TramiteArquivo, download = true) {
    this._arquivoService.downloadFile(tramiteArquivo.arquivo.hash, download);
  }

}
