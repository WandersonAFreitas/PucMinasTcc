import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { MatDialog, MatAutocompleteSelectedEvent } from '@angular/material';
import { SetorService } from '../../../setor.service';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { AssuntoArquivo } from '@fuse/types/models/assunto-arquivo';
import { Processo } from '@fuse/types/models/processo';
import { FluxoAcaoService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-acao/fluxo-acao.service';
import { ArquivoService } from '@fuse/core/arquivo.service';
import { ShellService } from '@fuse/core/shell.service';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { AssuntoArquivoService } from '@fuse/core/assunto-arquivo.service';

@Component({
  selector: 'processo-tab-modelos',
  templateUrl: './modelo-list.component.html',
  styleUrls: ['./modelo-list.component.css']
})
export class ProcessoModeloListComponent extends BaseListComponent<AssuntoArquivo> implements OnInit {

  @Input() public processo: Processo;

  public processoId: number;
  public displayedColumns: string[] = ['Arquivo.Nome', 'Actions'];

  public entityFilter: AssuntoArquivo;


  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    public _setorService: SetorService,
    public _acaoService: FluxoAcaoService,
    private _arquivoService: ArquivoService,
    private _shellService: ShellService,
    private _assuntoArquivoService: AssuntoArquivoService
  ) {
    super(_router, _assuntoArquivoService, _shellService);
    this.processoId = this.route.params['value'].id || 0;
    this.entityFilter = this.initFilter();
  }

  ngOnInit(): void {
    if (this.processo && this.processo.assuntoId) {
      this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Arquivo.Nome', 'asc');
      this.baseLoad(this.gridSettingsModel);
    }
  }

  private initialFilter(): Filter {
    const rules = [
      new Rule('AssuntoId', this.op.eq, '' + this.processo.assuntoId),
    ];

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear(this.initialFilter());
  }

  private initFilter(): AssuntoArquivo {
    const model: AssuntoArquivo = new AssuntoArquivo(null);
    return model;
  }

  public downloadArquivo(assuntoArquivo: AssuntoArquivo, download = true) {
    this._arquivoService.downloadFile(assuntoArquivo.arquivo.hash, download);
  }

}
