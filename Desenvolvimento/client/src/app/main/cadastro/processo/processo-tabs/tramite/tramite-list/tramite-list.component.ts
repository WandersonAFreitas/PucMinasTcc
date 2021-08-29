import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { MatDialog, MatAutocompleteSelectedEvent, MatTable } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { SetorService } from '../../../setor.service';
import { ProcessoTabTramiteDetailComponent, ITramiteArquivoViewModel } from '../tramite-detail/tramite-detail.component';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Tramite } from '@fuse/types/models/tramite';
import { Processo } from '@fuse/types/models/processo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ArquivoService } from '@fuse/core/arquivo.service';
import { FluxoAcaoService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-acao/fluxo-acao.service';
import { ShellService } from '@fuse/core/shell.service';
import { TipoSituacaoEnum } from '@fuse/types/models/enums/tipo-situacao-enum';
import { TramitarEmEnum } from '@fuse/types/models/enums/tramitar-em-enum';
import { TramiteService } from '@fuse/core/tramite.service';

@Component({
  selector: 'processo-tab-tramites',
  templateUrl: './tramite-list.component.html',
  styleUrls: ['./tramite-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ProcessoTramiteListComponent extends BaseListComponent<Tramite> implements OnInit {

  @ViewChild('table') table: MatTable<any>;

  @Output() notifyTramitado: EventEmitter<boolean> = new EventEmitter();

  @Input() public processo: Processo;

  public ehSituacaoFinal: boolean;

  public processoId: number;
  public displayedColumns: string[] = [
    'Actions',
    'Acao.Nome',
    'Setor.Nome',
    'SituacaoDoProcessoNoTramite.Nome',
    'Situacao.Nome',
    'Observacao',
    'Data'
  ];

  
  public displayedColumnsArquivos: string[] = ['ArquivoNome', 'Tipo', 'Obrigatorio', 'Assinatura', 'Actions'];

  public entityFilter: Tramite;

  public gridSettingsModelToFilterAutocompleteSetor: GridSettings;
  public gridSettingsModelToFilterAutocompleteAcao: GridSettings;

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    private _arquivoService: ArquivoService,
    public _setorService: SetorService,
    public _acaoService: FluxoAcaoService,
    private _shellService: ShellService,
    private _tramiteService: TramiteService
  ) {
    super(_router, _tramiteService, _shellService);
    this.processoId = this.route.params['value'].id || 0;
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Id', 'dsc');
    // this.initSetorSubject();
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
    this.initGridSettingsModelSetor();
    this.initGridSettingsModelAcao();

    if (this.processo && this.processo.situacao) {
      this.ehSituacaoFinal = this.processo.situacao.tipoSituacao == TipoSituacaoEnum.SituacaoFinal;
    }
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
      new Rule('ProcessoId', this.op.eq, '' + this.processoId),
    ];

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public delete(entity: Tramite): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Tramite?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          this._shellService.alert().success({ messages: ['Tramite removido com sucesso!'], timeout: 3000 });
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
        });
      }
    });
  }

  public callback = (dataSource) => {
    this.table.dataSource = dataSource.data;
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel, this.callback);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear(this.initialFilter());
  }

  private initFilter(): Tramite {
    const model: Tramite = new Tramite(null);
    return model;
  }

  public add(): void {
    const processoId = this.processoId;
    this._tramiteService.getHaTramiteNaoTramitadoNoProcesso(processoId).subscribe((temNaoTramitado) => {
      if (temNaoTramitado) {
        this._shellService.alert().warning({ messages: ['Há tramite em andamento para esse processo.'], timeout: 3000 });
      } else {
        this.openDialogToAdd();
      }
    });

  }

  private openDialogToAdd() {
    const dialogRef = this._dialog.open(ProcessoTabTramiteDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        processo: this.processo
      },
      disableClose: true
    });
    this.afterClosedRefreshGrids(dialogRef);
  }

  public edit(tramite: Tramite, detailsOnly = false): void {
    const processoId = this.processoId;
    const dialogRef = this._dialog.open(ProcessoTabTramiteDetailComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        detailsOnly,
        tramite,
        processo: this.processo
      },
      autoFocus: false,
      disableClose: true
    });

    this.afterClosedRefreshGrids(dialogRef);
  }

  private afterClosedRefreshGrids(dialogRef: any) {
    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Ok') {
        this.search();
      } else if (result === 'Tramitado') {
        this.notifyTramitado.emit(true);
      }
    });
  }

  public situacaoInicial(tramite: Tramite) {
    if (tramite.acao.fluxo.tramitarEm == TramitarEmEnum.FluxoDefinido) {
      return tramite.acao.fluxo.fluxoItems.find(x => x.acaoId == tramite.acaoId).situacaoAtual.nome;
    }
    return '-'
  }

  public getRows(table: MatTable<any>, data: Array<any>) {
    if (data) {
      if (table.dataSource && (table.dataSource['length'] === data.length)) {
        let rows = [];
        data.forEach((element: any) => rows.push(element, { detailRow: true, element }));
        table.dataSource = rows;
      } else if (!table.dataSource && table['_headerRowDefs'] && data.length) {
        table.dataSource = data;
      }
    }
  }
  public isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');

  public toggleRow(table: MatTable<any>, index: number, event: MouseEvent) {
    if (event.target['tagName'] === "MAT-CELL") {
      table.dataSource[index + 1]['element']['show_' + index] = !table.dataSource[index + 1]['element']['show_' + index];
    }
  }

  public downloadArquivo(arquivo: ITramiteArquivoViewModel, download = true) {
    this._arquivoService.downloadFile(arquivo.hash, download);
  }
}
