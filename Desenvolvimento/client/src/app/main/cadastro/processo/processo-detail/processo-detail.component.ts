import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ViewChild, QueryList, ViewChildren } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { ProcessoService } from '../processo.service';
import { EmpresaService } from '../../empresa/empresa.service';
import { AssuntoService } from '../../assunto/assunto.service';
import { MatDialog } from '@angular/material';
import { ProcessoAtribuirComponent } from '../processo-atribuir/processo-atribuir.component';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { Processo } from '@fuse/types/models/processo';
import { Assunto } from '@fuse/types/models/assunto';
import { CredentialsService } from '@fuse/core/credentials.service';
import { ShellService } from '@fuse/core/shell.service';
import { TramiteService } from '@fuse/core/tramite.service';
import { TipoSituacaoEnum } from '@fuse/types/models/enums/tipo-situacao-enum';
import { User } from '@fuse/types/models/user';
import { padLeft } from '@fuse/utils/utils';

@Component({
  templateUrl: './processo-detail.component.html',
  styleUrls: ['./processo-detail.component.css'],
})
export class ProcessoDetailComponent implements OnInit {

  public displayedColumns: string[] = ['Nome', 'Sigla', 'Ativo', 'Action'];
  public id: number;
  public formGroup: FormGroup;
  public expandedElement: any;

  public ehSituacaoFinal: boolean;
  public assuntoOrientacoes: string;
  public gridSettingsModelEmpresa: GridSettings;
  public gridSettingsModelAssunto: GridSettings;

  public entity: Processo = new Processo(null);
  public cmbAssuntos = new Array<Assunto>();

  constructor(
    private _router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    public _empresaService: EmpresaService,
    public _assuntoService: AssuntoService,
    private _processoService: ProcessoService,
    private _credentialsService: CredentialsService,
    private _shellService: ShellService,
    private _tramiteService: TramiteService,
  ) {
    this.initialize();
    this.createFormValidators();
  }

  ngOnInit() {
    this.init();
  }
  private init(): void {
    this.route.params.subscribe(params => {
      this._shellService.blockUI();
      if (params.id) {
        this.id = params.id;
        forkJoin(
          this._processoService.get(this.id, false),
        ).pipe(
          finalize(() => this._shellService.unblockUI())
        ).subscribe(([entity]) => {
          this.entity = entity;
          this.ehSituacaoFinal = this.entity.situacao.tipoSituacao == TipoSituacaoEnum.SituacaoFinal;
          const temTramite = !!(this.entity && this.entity.tramites && this.entity.tramites.length);
          this.createFormValidators(temTramite);
          this.initGridSettingsModels();
        })
      } else {
        this._shellService.unblockUI();
        this.createFormValidators();
        this.initGridSettingsModels();
      }
    });
  }

  private initGridSettingsModels() {
    this.initGridSettingsModelEmpresa();
    this.id && this.initGridSettingsModelAssunto();
    const empresaIdControl = this.formGroup.get('empresaId');
    empresaIdControl.valueChanges.subscribe((empresaId) => {
      this.clearAssuntoSituacao();
      this.initGridSettingsModelAssunto();
    });

    const assuntoIdControl = this.formGroup.get('assuntoId');
    assuntoIdControl.valueChanges.subscribe((assuntoId) => {
      if (assuntoId) {
        this._assuntoService.get(assuntoId).subscribe(assunto => {
          this.assuntoOrientacoes = assunto.orientacoes;
          if (assunto.fluxo && assunto.fluxo.situacoes) {
            const situacoes = assunto.fluxo.situacoes.sort(this.sortById());
            const primeiraSituacao = situacoes[0]; // TODO: VERIFICAR SE O PRIMEIRO NA LISTA É REALMENTE O PRIMEIRO NO FLUXO
            this.formGroup.get('situacaoNome').setValue(primeiraSituacao.nome);
            this.formGroup.get('situacaoId').setValue(primeiraSituacao.id);
          } else {
            this.clearAssuntoSituacao();
            this._shellService.alert().warning({ messages: ['Assunto sem <b>fluxo</b> cadastrado!'], timeout: 5000 });
          }
        });
      } else {
        this.clearAssuntoSituacao();
      }
    });
  }

  private clearAssuntoSituacao() {
    this.formGroup.get('assuntoId').value && this.formGroup.get('assuntoId').setValue(null);
    this.formGroup.get('assuntoNome').value && this.formGroup.get('assuntoNome').setValue(null);
    this.formGroup.get('situacaoNome').value && this.formGroup.get('situacaoNome').setValue('-');
    this.formGroup.get('situacaoId').value && this.formGroup.get('situacaoId').setValue(null);
  }

  private sortById() {
    return (a, b) => a.id < b.id ? -1 : (a.id > b.id ? 1 : 0);
  }

  private initGridSettingsModelEmpresa() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleSigla = new Rule('Sigla', 'cn', null);
    const filter = new Filter(1, [ruleNome, ruleSigla]);
    this.gridSettingsModelEmpresa = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  public initGridSettingsModelAssunto() {
    const ruleNome = new Rule('Nome', 'cn', null);
    let filter = new Filter(0, [ruleNome]);
    const empresaId = this.formGroup.controls.empresaId.value;
    if (empresaId) {
      const ruleEmpresa = new Rule('EmpresaId', 'eq', '' + empresaId);
      const ruleAssuntoAtivo = new Rule('Ativo', 'eq', '' + true);
      const ruleFluxoAtivo = new Rule('Fluxo.Ativo', 'eq', '' + true);
      filter = new Filter(0, [ruleNome], [new Filter(0, [ruleEmpresa, ruleAssuntoAtivo, ruleFluxoAtivo])]);
    }

    this.gridSettingsModelAssunto = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this._assuntoService.getByFilter(this.gridSettingsModelAssunto).subscribe(page => {
      this.cmbAssuntos = page.content;
      if(!page.totalElements) {
        this._shellService.alert().warning({ messages: ['A Empresa selecionada <b>NÃO</b> contém Assuntos ativos ou <b>NÃO</b> há Assuntos em Fluxo Ativo'], timeout: 5000 });
      }
    });
  }

  private createFormValidators(disabled = false): void {
    this.formGroup = this._fb.group({
      id: [this.entity.id],
      sequencialAno: [{ value: (this.entity.sequencial && this.entity.ano) ? `${padLeft(this.entity.sequencial, 10, '0')}/${this.entity.ano}` : '-', disabled: true }],
      sequencial: [this.entity.sequencial],
      ano: [this.entity.ano],
      responsavelId: [this.id ? this.entity.responsavelId : this._credentialsService.authenticatedUser.id],
      responsavelNome: [{ value: this.id ? (this.entity.responsavel && this.entity.responsavel.userName) || 'AGUARDANDO ATRIBUIÇÃO' : this._credentialsService.authenticatedUser.userName, disabled: true }],
      setorId: [this.entity && this.entity.setorId],
      setorNome: [{ value: this.entity && this.entity.setor && this.entity.setor.nome || '-', disabled: true }],
      situacaoNome: [{ value: this.entity.situacao && this.entity.situacao.nome || '-', disabled: true }],
      situacaoId: [this.entity.situacaoId || null],
      empresaNome: [{ value: this.entity.empresa && this.entity.empresa.nome, disabled: disabled }],
      empresaId: [{ value: this.entity.empresaId, disabled: disabled }],
      assuntoNome: [{ value: this.entity.assunto && this.entity.assunto.nome, disabled: disabled }],
      assuntoId: [{ value: this.entity.assuntoId, disabled: disabled }],
      descricao: [this.entity.descricao],
      processoAutores: this._fb.array([]),
    });

    this.assuntoOrientacoes = this.entity.assunto && this.entity.assunto.orientacoes;
  }

  public save(formModel: Processo, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._processoService.update(this.entity).subscribe(
          () => {
            //this.goBack();
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._processoService.save(this.entity).subscribe(
          (processo) => {
            this._router.navigate(['/cadastro/processo/edit', processo.id]);
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Processo): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    // this.location.back();
    this._router.navigate(['/cadastro/processo/todos']);
  }

  private initialize(): void {
    this.entity = new Processo(null);
  }

  public selectedIndexChange(event: number) {
    if (event === 0) {
      // this.closeAll();
    }
  }

  public tramitar(): void {
    this._processoService.getTramiteValidoDoProcesso(this.id).subscribe(tramite => {
      if (tramite) {
        this._shellService.confirm().confirm({ message: `Deseja tramitar com a ação "${tramite.acao.nome.toUpperCase()}" para o setor "${tramite.setor.nome.toUpperCase()}"? Após confimar o tramite, não será possível fazer alterações no tramite.`, title: 'Tramitar' })
          .subscribe(res => {
            if (res) {
              this._tramiteService.getTramitar(tramite.id).subscribe(result => {
                if (result.tramitado) {
                  this._shellService.alert().success({ messages: ['Tramitado com sucesso!'], timeout: 3000 });
                  this.goBack();
                } else {
                  this._shellService.alert().warning({ messages: ['Ocorreu um erro ao tramitar!'], timeout: 5000 });
                }
              });
            }
          })
      } else {
        this._shellService.alert().warning({ messages: ['Não há tramite valido!'], timeout: 5000 });
      }
    })
  }


  public receber(): void {
    this._shellService.confirm().confirm({ message: `Deseja receber o processo da Empresa "${this.entity.empresa.nome}" ?`, title: 'Receber' })
      .subscribe(res => {
        if (res) {
          const idResponsavel = this._credentialsService.authenticatedUser.id;
          this._processoService.getReceber(this.entity.id, idResponsavel).subscribe(result => {
            if (result.responsavelId === idResponsavel) {
              this._shellService.alert().success({ messages: ['Recebido com sucesso!'], timeout: 3000 });
              this.entity.responsavelId = result.responsavelId;
              this.formGroup.get('responsavelId').setValue(this.entity.responsavelId);
              this.formGroup.get('responsavelNome').setValue(result.responsavel.userName);
            } else {
              this._shellService.alert().warning({ messages: ['Ocorreu um erro ao receber!'], timeout: 5000 });
            }
          });
        }
      })
  }

  // public getRows(table: MatTable<any>, data: Array<any>) {
  //   if (data) {
  //     const rows = [];
  //     data.forEach((element: any) => rows.push(element, { detailRow: true, element }));
  //     return rows;
  //   }
  // }

  public atribuir() {
    const dialogRef = this._dialog.open(ProcessoAtribuirComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        processo: this.entity
      },
      disableClose: false
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result && result !== 'result') {
        const responsavel: User = result;
        this.formGroup.get('responsavelId').setValue(responsavel.id);
        this.formGroup.get('responsavelNome').setValue(responsavel.userName);
      }
    });
  }
}
