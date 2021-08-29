import { Component, OnInit, ViewChild, Inject, ElementRef, AfterViewInit, ViewChildren, QueryList, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { MatSelectionList, MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent, MatTableDataSource, MatCheckbox, MatSelect } from '@angular/material';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { Tramite } from '@fuse/types/models/tramite';
import { FluxoItemCheckList } from '@fuse/types/models/fluxo-item-checklist';
import { FluxoItemAnexo } from '@fuse/types/models/fluxo-item-anexo';
import { TramitarEmEnum, TramitarEmEnumArrayKeyValue, TramitarEm } from '@fuse/types/models/enums/tramitar-em-enum';
import { FluxoAcao } from '@fuse/types/models/fluxo-acao';
import { Setor } from '@fuse/types/models/setor';
import { Situacao } from '@fuse/types/models/situacao';
import { TramiteService } from '@fuse/core/tramite.service';
import { ArquivoService } from '@fuse/core/arquivo.service';
import { SetorService } from 'app/main/cadastro/empresa/setor.service';
import { FluxoAcaoService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-acao/fluxo-acao.service';
import { FluxoSituacaoService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-situacao/fluxo-situacao.service';
import { FluxoItemService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-item/fluxo-item.service';
import { FluxoItemSetorService } from 'app/main/cadastro/fluxo/fluxo-tabs/fluxo-item/fluxo-item-tabs/fluxo-item-setor.service';
import { ShellService } from '@fuse/core/shell.service';
import { CredentialsService } from '@fuse/core/credentials.service';
import { Processo } from '@fuse/types/models/processo';
import { FluxoItem } from '@fuse/types/models/fluxo-item';

export interface ITramiteArquivoViewModel {
  id: number;
  nome: string;
  hash: string;
  tipo: string;
  tramiteId: number;
  arquivoId: number;
  fluxoItemTipoAnexoId: number;
  exigeAssinaturaDigital: boolean;
  obrigatorio: boolean;
}

@Component({
  selector: 'processo-tab-tramites-detail',
  templateUrl: './tramite-detail.component.html',
  styleUrls: ['./tramite-detail.component.css']
})
export class ProcessoTabTramiteDetailComponent implements OnInit, AfterViewInit {

  @ViewChild('fileDocumentInput') fileDocumentInput: ElementRef;
  @ViewChild('tipoAnexoSelect') tipoAnexoSelect: MatSelect;

  @ViewChildren('checkBoxList') checkBoxList: QueryList<MatCheckbox>;

  public id: number;
  public acaoId: number;
  public formGroup: FormGroup;
  public gridSettingsModel: GridSettings;

  public displayedColumnsArquivos: string[] = ['ArquivoNome', 'Tipo', 'Obrigatorio', 'Assinatura', 'Actions'];
  public dataSourceArquivos: MatTableDataSource<ITramiteArquivoViewModel>;
  public files = new Array<File>();
  public arquivos = new Array<ITramiteArquivoViewModel>();

  private tramite: Tramite;
  private processoId: number;
  private fluxoItemTipoAnexoId: number;

  public gridSettingsModelAcao: GridSettings;
  public gridSettingsModelSetor: GridSettings;
  public gridSettingsModelSituacao: GridSettings;

  public temfluxoItemChecklists: boolean;
  public fluxoItemChecklists: FluxoItemCheckList[];
  public fluxoItemAnexos: FluxoItemAnexo[];

  public ehFluxoDefinido = TramitarEmEnum.FluxoDefinido;
  public canAddOrUpdate = true;

  public cmbAcoes = new Array<FluxoAcao>();
  public cmbSetores = new Array<Setor>();
  public cmbSituacoes = new Array<Situacao>();

  constructor(
    private cdRef: ChangeDetectorRef,
    public thisDialogRef: MatDialogRef<ProcessoTabTramiteDetailComponent>,
    private _service: TramiteService,
    private _arquivoService: ArquivoService,
    public _setorService: SetorService,
    public _fluxoAcaoService: FluxoAcaoService,
    public _fluxoSituacaoService: FluxoSituacaoService,
    public _fluxoItemService: FluxoItemService,
    public _fluxoItemSetorService: FluxoItemSetorService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    private _credentialsService: CredentialsService,
    @Inject(MAT_DIALOG_DATA) public data: { tramite: Tramite, processo: Processo, detailsOnly: boolean }
  ) {
    this.initialize();
    this.tramite = this.data.tramite;
    this.processoId = this.data.processo.id;
    this.id = this.data.tramite && this.data.tramite.id;
    this.createFormValidators();
    this.initGridSettingsModels();
  }

  ngOnInit() {
    if (this.tramite) {
      if (this.tramite.tramiteArquivos) {
        this.arquivos = this.tramite.tramiteArquivos.map(x => <ITramiteArquivoViewModel>{
          id: x.id,
          nome: x.arquivo.nome,
          hash: x.arquivo.hash,
          tipo: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.fluxoTipoAnexo && x.fluxoItemTipoAnexo.fluxoTipoAnexo.nome),
          tramiteId: x.tramiteId,
          arquivoId: x.arquivoId,
          fluxoItemTipoAnexoId: x.fluxoItemTipoAnexoId,
          exigeAssinaturaDigital: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.exigeAssinaturaDigital) || false,
          obrigatorio: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.obrigatorio) || false,
        });
        this.dataSourceArquivos = new MatTableDataSource<ITramiteArquivoViewModel>(this.arquivos);
      }
      this.createFormValidators();
      this.initGridSettingsModels();
    }
  }

  ngAfterViewInit() {
    this.canAddOrUpdate = !this.temCheckBoxParaChecar();
  }

  private createFormValidators(): void {
    this.acaoId = this.tramite && this.tramite.acaoId;
    this.formGroup = this._fb.group({
      id: [{ value: this.tramite && this.tramite.id, disabled: this.data.detailsOnly }],
      processoId: [{ value: this.processoId, disabled: this.data.detailsOnly }],
      acaoId: [{ value: this.tramite && this.tramite.acaoId, disabled: this.data.detailsOnly }],
      acaoNome: [{ value: this.tramite && this.tramite.acao && this.tramite.acao.nome, disabled: this.data.detailsOnly }],
      setorId: [{ value: this.tramite && this.tramite.setorId, disabled: this.data.detailsOnly }],
      setorNome: [{ value: this.tramite && this.tramite.setor && this.tramite.setor.nome, disabled: this.data.detailsOnly }],
      situacaoId: [{ value: this.tramite && this.tramite.situacaoId, disabled: this.data.detailsOnly }],
      situacaoDoProcessoNoTramiteId: [{ value: (this.tramite && this.tramite.situacaoDoProcessoNoTramiteId) || this.data.processo.situacaoId, disabled: this.data.detailsOnly }],
      situacaoNome: [{ value: this.tramite && this.tramite.situacao && this.tramite.situacao.nome, disabled: this.data.detailsOnly }],
      observacao: [{ value: this.tramite && this.tramite.observacao, disabled: this.data.detailsOnly }],
      tramiteChecklists: this._fb.array([]),
      // tramiteArquivos: this._fb.array([]),
      responsavelId: [{ value: (this.tramite && this.tramite.responsavelId) || this._credentialsService.authenticatedUser.id, disabled: this.data.detailsOnly }],
      enviarEmailsPara: [{ value: (this.tramite && this.tramite.enviarEmailsPara) || 'email@teste.com', disabled: this.data.detailsOnly }], // TODO: ALTERAR
      enviarEmailAutores: [{ value: (this.tramite && this.tramite.enviarEmailAutores) || false, disabled: this.data.detailsOnly }],
    });
  }

  public save(formModel: Tramite, isValid: boolean, tramitar = false): void {
    if (isValid) {
      this.prepareToSave(formModel);
      this.tramite.tramitado = tramitar;
      if (this.tramite.id) {
        this._service.update(this.tramite).subscribe(
          () => {
            this.salvarArquivos().then(x => {
              tramitar ? this.thisDialogRef.close('Tramitado') : this.thisDialogRef.close('Ok');
              this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
            });
          });
      } else {
        this._service.save(this.tramite).subscribe(
          (tramite) => {
            this.id = tramite.id;
            this.salvarArquivos().then(x => {
              tramitar ? this.thisDialogRef.close('Tramitado') : this.thisDialogRef.close('Ok');
              this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
            });
          });
      }
    }
  }

  private prepareToSave(formModel: Tramite): void {
    this.tramite = { ...this.tramite, ...formModel };
  }

  private initialize(): void {
    this.initGridSettingsModel();
    this.tramite = new Tramite(null);
  }

  private initGridSettingsModel() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  public openSelectFile(fluxoItemTipoAnexoId: number) {
    // const tipoAnexoSelect = this.tipoAnexoSelect;
    this.fluxoItemTipoAnexoId = fluxoItemTipoAnexoId;
    this.fileDocumentInput.nativeElement['click']();
  }

  public anexaArquivoInMemory(event: any) {
    const fluxoItemTipoAnexoId = this.fluxoItemTipoAnexoId;
    if (event.target.files[0] !== undefined) {
      const file = event.target.files[0];
      this.fileDocumentInput.nativeElement['value'] = '';

      this.files.push(file);

      if (!fluxoItemTipoAnexoId) {
        const novoArquivo = <ITramiteArquivoViewModel>{
          id: null,
          nome: file.name,
          hash: null,
          tipo: null,
          tramiteId: null,
          arquivoId: null,
          fluxoItemTipoAnexoId: null,
          exigeAssinaturaDigital: false,
          obrigatorio: false,
        };
        this.arquivos.push(novoArquivo);
      } else {
        this.arquivos.filter(x => {
          if (x.fluxoItemTipoAnexoId == fluxoItemTipoAnexoId) {
            x.nome = file.name;
          }
        });
      }

      this.dataSourceArquivos = new MatTableDataSource<ITramiteArquivoViewModel>(this.arquivos);
    }
    this.fluxoItemTipoAnexoId = null;
  }

  public async salvarArquivos() {
    if (this.files && this.files.length > 0) {

      const arquivos = this.arquivos.filter(arquivo => {
        const file = this.files.find(x => x.name == arquivo.nome);
        if (file) {
          return true;
        }
        return false;
      });

      const arquivoArray = await this._arquivoService.uploadTramiteArquivosPromise(
        this.files,
        false,
        arquivos,
        this.id
        // `tramite/${this.id}`
      );

      if (arquivoArray.length === this.files.length) {
        for (const arquivo of arquivoArray) {
          const documento = this.arquivos.find((doc, index, arr) => doc.nome === arquivo.nome);
          documento.hash = arquivo.hash;
        }
      } else {
        this._shellService.alert().error({
          timeout: 5000,
          messages: ['Ocorreu a falha em salvar ' + (this.arquivos.length - arquivoArray.length) + ' arquivo(s)']
        });
      }
    }
    return undefined;
  }

  public deletaArquivo(arquivo: ITramiteArquivoViewModel) {
    this._shellService.confirm().confirm({ message: `Deseja remover o modelo: "${arquivo.nome}" ?`, title: 'Remover modelo' })
      .subscribe(res => {
        if (res) {
          if (arquivo.id) {
            this._arquivoService.remove(arquivo.id, false).subscribe(() => {
              this.files.splice(this.files.indexOf(this.files.find((file, index, arr) => arquivo.nome === file.name)), 1);
              this.arquivos.splice(this.arquivos.indexOf(arquivo), 1);
              this.dataSourceArquivos = null;
              this.dataSourceArquivos = new MatTableDataSource<ITramiteArquivoViewModel>(this.arquivos);
            });
          } else {
            this.files.splice(this.files.indexOf(this.files.find((file, index, arr) => arquivo.nome === file.name)), 1);
            this.arquivos.splice(this.arquivos.indexOf(arquivo), 1);
            this.dataSourceArquivos = null;
            this.dataSourceArquivos = new MatTableDataSource<ITramiteArquivoViewModel>(this.arquivos);
          }
        }
      })
  }

  public downloadArquivo(arquivo: ITramiteArquivoViewModel, download = true) {
    this._arquivoService.downloadFile(arquivo.hash, download);
  }


  private initGridSettingsModels() {
    this.initGridSettingsModelAcao();
    this.initGridSettingsModelSetor();
    this.initGridSettingsModelSituacao();

    if (this.data.processo.assunto.fluxo.tramitarEm == this.ehFluxoDefinido) {
      const acaoIdControl = this.formGroup.get('acaoId');
      acaoIdControl.valueChanges.subscribe((acaoId) => {
        this.clearSetor();
        this.acaoId = acaoId;
        this.formGroup.setControl('tramiteChecklists', this._fb.array([]));
        this.initGridSettingsModelSetor(); // this.updateGridSettingsModelSetor();
        // this.formGroup.get('setorNome').setValue(null); // força o trigger em setorNome do app-input-autocomplete (this.c.valueChanges)
      });
    }
  }

  private clearSetor() {
    this.formGroup.get('setorId').value && this.formGroup.get('setorId').setValue(null);
    // this.formGroup.get('setorNome').value && this.formGroup.get('setorNome').setValue(null);
  }

  private async initGridSettingsModelAcao() {
    const tramitarEm = TramitarEmEnumArrayKeyValue.find(x => x.key === this.data.processo.assunto.fluxo.tramitarEm);

    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleFluxoId = new Rule('FluxoId', 'eq', '' + this.data.processo.assunto.fluxoId);
    // let filter = new Filter(0, [ruleNome], [new Filter(0, [ruleFluxoId])]);
    let filter = new Filter(0, [ruleNome, ruleFluxoId]);

    if (tramitarEm.key === TramitarEmEnum.FluxoDefinido) {
      const situacaoId = this.data.processo.situacaoId;
      situacaoId && await this._fluxoItemService.getFluxoItensPorFluxoEsituacao(this.data.processo.assunto.fluxo.id, situacaoId).toPromise().then(result => {
        const ruleAcoesId = result.map(x => new Rule('AcaoId', 'eq', '' + x.acaoId));
        if (ruleAcoesId.length) {
          filter = new Filter(0, [ruleNome, ruleFluxoId], [new Filter(1, ruleAcoesId)]);
          this._fluxoAcaoService.getByFilter(this.gridSettingsModelAcao).subscribe(pageAcoes => {
            this.cmbAcoes = pageAcoes.content;
            this.msgDeFluxoSemAcao();
          });
        } else {
          if (this.data.detailsOnly) {
            this.cmbAcoes = this.cmbAcoes.concat(new FluxoAcao(this.data.tramite.acaoId, this.data.tramite.acao.nome));
            this.msgDeFluxoSemAcao();
          } else {
            this.onCloseCancel();
            this._shellService.alert().warning({ messages: [`Processo com status "${this.data.processo.situacao.nome}"`], timeout: 5000 });
          }
        }
      });
    } else {
      this.gridSettingsModelAcao = new GridSettings(true, 0, 10, 'Nome', 'asc', filter);
      this._fluxoAcaoService.getByFilter(this.gridSettingsModelAcao).subscribe(pageAcoes => {
        this.cmbAcoes = pageAcoes.content;
        this.msgDeFluxoSemAcao();
      });
    }
  }

  private msgDeFluxoSemAcao() {
    if (!this.cmbAcoes.length) {
      this._shellService.alert().warning({ messages: [`<b>NÃO</b> há ação cadastrada no fluxo.`], timeout: 5000 });
    }
  }

  private async initGridSettingsModelSetor() {
    const tramitarEm = TramitarEmEnumArrayKeyValue.find(x => x.key === this.data.processo.assunto.fluxo.tramitarEm);

    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);

    let filter = new Filter(1, []);

    if (tramitarEm.key === TramitarEmEnum.SetoresDaEmpresaDoProcesso) {
      const ruleEmpresaId = new Rule('EmpresaId', 'eq', '' + this.data.processo.empresaId);
      filter = new Filter(1, [ruleNome, ruleSigla], [new Filter(0, [ruleEmpresaId])]);
    } else if (tramitarEm.key === TramitarEmEnum.FluxoDefinido) {
      await this._fluxoItemSetorService.getItemSetoresPorFluxo(this.data.processo.assunto.fluxo.id).toPromise().then(result => {
        const ruleIdSetorArray = result.filter(x => x.fluxoItem.acaoId === this.acaoId).map(x => new Rule('Id', 'eq', '' + x.setor.id));
        filter = new Filter(1, [ruleNome, ruleSigla], [new Filter(1, ruleIdSetorArray)]);
      });

      const acaoIdSelecionado = this.formGroup.get('acaoId').value;
      acaoIdSelecionado && await this._fluxoItemService.getFluxoItensPorFluxo(this.data.processo.assunto.fluxo.id, acaoIdSelecionado).toPromise().then(result => {
        this.getFluxoItemChecklists(result);
        this.getFluxoItemAnexos(result);
      });
    } else { // 'Entre setores de todas as empresas'
      filter = new Filter(1, [ruleNome, ruleSigla]);
    }
    this.gridSettingsModelSetor = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this._setorService.getByFilter(this.gridSettingsModelSetor).subscribe(page => {
      this.cmbSetores = page.content;
    });
  }

  private async initGridSettingsModelSituacao() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleFluxoId = new Rule('FluxoId', 'eq', '' + this.data.processo.assunto.fluxoId);
    const filter = new Filter(1, [ruleNome], [new Filter(0, [ruleFluxoId])]);
    this.gridSettingsModelSituacao = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this._fluxoSituacaoService.getByFilter(this.gridSettingsModelSituacao).subscribe(page => {
      this.cmbSituacoes = page.content;
    });
  }

  private async updateGridSettingsModelSetor() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);

    let filter = new Filter(1, []);

    const acaoIdSelecionado = this.formGroup.get('acaoId').value;

    if (acaoIdSelecionado) {
      await this._fluxoItemSetorService.getItemSetoresPorFluxo(this.data.processo.assunto.fluxo.id).toPromise().then(result => {
        const ruleIdSetorArray = result.filter(x => x.fluxoItem.acaoId === acaoIdSelecionado).map(x => new Rule('Id', 'eq', '' + x.setor.id));
        filter = new Filter(1, [ruleNome, ruleSigla], [new Filter(1, ruleIdSetorArray)]);
      });

      const tramitarEm = TramitarEmEnumArrayKeyValue.find(x => x.key === this.data.processo.assunto.fluxo.tramitarEm);
      if (tramitarEm.key === TramitarEmEnum.FluxoDefinido) {
        await this._fluxoItemService.getFluxoItensPorFluxo(this.data.processo.assunto.fluxo.id, acaoIdSelecionado).toPromise().then(result => {
          this.getFluxoItemChecklists(result);
          this.getFluxoItemAnexos(result);
        });
      }
    }

    this.gridSettingsModelSetor = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  private getFluxoItemChecklists(fluxoItens: FluxoItem[]) {
    if (fluxoItens.length) {
      const fluxoItemChecklist = fluxoItens.map(x => x.fluxoItemChecklists).reduce((a: FluxoItemCheckList[], c: FluxoItemCheckList[]) => a.concat(c), new Array<FluxoItemCheckList>());
      this.fluxoItemChecklists = fluxoItemChecklist;

      this.formGroup.setControl('tramiteChecklists', this._fb.array([]));

      if (this.tramite && this.tramite.id) { // this.formGroup.get('acaoId').value == this.tramite.acaoId) {
        this.tramite.tramiteChecklists.forEach(x => {
          const controlTramiteChecklists = <FormArray>this.formGroup.controls.tramiteChecklists;
          controlTramiteChecklists.push(this._fb.group({
            id: [x.id],
            nome: [this.fluxoItemChecklists.find(t => t.id == x.fluxoItemChecklistId).nome],
            checado: [{ value: x.checado, disabled: this.data.detailsOnly }],
            fluxoItemChecklistId: [x.fluxoItemChecklistId],
            tramiteId: [x.tramiteId],
          }));
        });
      } else {
        this.fluxoItemChecklists.forEach(x => {
          const controlTramiteChecklists = <FormArray>this.formGroup.controls.tramiteChecklists;
          controlTramiteChecklists.push(this._fb.group({
            id: [null],
            nome: [x.nome],
            checado: [false],
            fluxoItemChecklistId: [x.id],
            tramiteId: [null],
          }));
        });
      }
    }
  }

  private getFluxoItemAnexos(fluxoItens: FluxoItem[]) {
    this.dataSourceArquivos = null;
    this.fluxoItemAnexos = new Array<FluxoItemAnexo>();
    this.files = new Array<File>();
    const arquivos = new Array<ITramiteArquivoViewModel>();
    this.arquivos = arquivos;

    if (fluxoItens.length) {
      const fluxoItemAnexos = fluxoItens.map(x => x.fluxoItemTiposAnexo).reduce((a: FluxoItemAnexo[], c: FluxoItemAnexo[]) => a.concat(c), new Array<FluxoItemAnexo>());
      this.fluxoItemAnexos = fluxoItemAnexos;
      // this.formGroup.setControl('tramiteArquivos', this._fb.array([]));

      if (this.tramite && this.tramite.id) {
        this.tramite.tramiteArquivos.forEach(x => {
          const arquivo = <ITramiteArquivoViewModel>{
            id: x.id,
            nome: x.arquivo.nome,
            hash: x.arquivo.hash,
            tipo: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.fluxoTipoAnexo && x.fluxoItemTipoAnexo.fluxoTipoAnexo.nome),
            tramiteId: x.tramiteId,
            arquivoId: x.arquivoId,
            fluxoItemTipoAnexoId: x.fluxoItemTipoAnexoId,
            exigeAssinaturaDigital: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.exigeAssinaturaDigital) || false,
            obrigatorio: (x.fluxoItemTipoAnexo && x.fluxoItemTipoAnexo.obrigatorio) || false,
          }
          arquivos.push(arquivo);
          // const controlTramiteArquivos = <FormArray>this.formGroup.controls.tramiteArquivos;
          // controlTramiteArquivos.push(this._fb.group({
          //   id: [x.id],
          //   nome: [x.arquivo.nome],
          //   hash: [x.arquivo.hash],
          //   tipo: [x.fluxoItemTipoAnexo.fluxoTipoAnexo.nome],
          //   tramiteId: [x.tramiteId],
          //   arquivoId: [x.arquivoId],
          //   fluxoItemTipoAnexoId: [x.fluxoItemTipoAnexoId],
          //   exigeAssinaturaDigital: [x.fluxoItemTipoAnexo.exigeAssinaturaDigital],
          //   obrigatorio: [x.fluxoItemTipoAnexo.obrigatorio],
          // }));
        });
      } else {
        this.fluxoItemAnexos.forEach(x => {
          const arquivo = <ITramiteArquivoViewModel>{
            id: null,
            nome: null,
            hash: null,
            tipo: x.fluxoTipoAnexo.nome,
            tramiteId: null,
            arquivoId: null,
            fluxoItemTipoAnexoId: x.id,
            exigeAssinaturaDigital: x.exigeAssinaturaDigital,
            obrigatorio: x.obrigatorio,
          };

          arquivos.push(arquivo);

          // const controlTramiteArquivos = <FormArray>this.formGroup.controls.tramiteArquivos;
          // controlTramiteArquivos.push(this._fb.group({
          //   id: [null],
          //   nome: [null],
          //   hash: [null],
          //   tipo: [x.fluxoTipoAnexo.nome],
          //   tramiteId: [null],
          //   arquivoId: [null],
          //   fluxoItemTipoAnexoId: [x.id],
          //   exigeAssinaturaDigital: [false],
          //   obrigatorio: [false],
          // }));
        });
      }

      this.arquivos = arquivos;
      this.dataSourceArquivos = new MatTableDataSource<ITramiteArquivoViewModel>(this.arquivos);
    }
  }

  public getTramitarEm(): TramitarEm {
    return TramitarEmEnumArrayKeyValue.find(x => x.key === this.data.processo.assunto.fluxo.tramitarEm);
  }

  public temCheckBoxParaChecar() {
    return this.checkBoxList && this.checkBoxList.some(x => !x.checked);
  }

  public temFluxoItemChecklists() {
    return this.fluxoItemChecklists && this.fluxoItemChecklists.length;
  }

  public temAnexoObrigatorioParaEnviar() {
    return this.dataSourceArquivos && this.dataSourceArquivos.data && this.dataSourceArquivos.data.some(x => x.obrigatorio && !x.nome);
  }
}
