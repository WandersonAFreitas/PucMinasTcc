import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { InsumoService } from '../insumo.service';
import { Insumo } from '@fuse/types/models/insumo';
import { ShellService } from '@fuse/core/shell.service';
import { UserService } from '@fuse/core/user.service';
import { CredentialsService } from '@fuse/core/credentials.service';
import { UnidadeMedidaService } from '../../unidademedida/unidademedida.service';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { MarcaService } from '../../marca/marca.service';
import { TipoInsumoService } from '../../tipoinsumo/tipoinsumo.service';
import { SetorService } from '../../empresa/setor.service';
import { FornecedorService } from '../../fornecedor/fornecedor.service';

@Component({
  templateUrl: './insumo-detail.component.html',
  styleUrls: ['./insumo-detail.component.css']
})
export class InsumoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  private entity: Insumo;
  public gridSettingsModel: GridSettings;
  public gridSettingsNomeModel: GridSettings;
  public gridSettingsTipoInsumoModel: GridSettings;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: InsumoService,
    private _shellService: ShellService,
    public  _userService: UserService,
    public  _unidadeMedidaService: UnidadeMedidaService,
    public  _marcaService: MarcaService,
    public  _tipoInsumoService: TipoInsumoService,
    public  _setorService: SetorService,
    public  _fornecedorService: FornecedorService,
    private _credentialsService: CredentialsService,
  ) {
    this.initialize();
    this.createFormValidators();
  }

  ngOnInit() {
    const ruleDescricao = new Rule('Descricao', 'cn', null);
    const filterDescricao = new Filter(1, [ruleDescricao]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Descricao', 'asc', filterDescricao);
    
    const ruleNome = new Rule('Nome', 'cn', null);
    const filterNome = new Filter(1, [ruleNome]);
    this.gridSettingsNomeModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filterNome);
    
    const ruleTipoInsumo = new Rule('Descricao', 'cn', null);
    const ruleTipoInsumoAtivo = new Rule('Ativo', 'eq', '' + true);
    const filterTipoInsumo = new Filter(0, [ruleTipoInsumo], [new Filter(0, [ruleTipoInsumoAtivo])]);
    this.gridSettingsTipoInsumoModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filterTipoInsumo);
    

    this.init();
  }

  private init(): void {
    this.route.params.subscribe(params => {
      this._shellService.blockUI();
      if (params.id) {
        this.id = params.id;
        forkJoin(
          this._service.get(this.id, false),
        ).pipe(
          finalize(() => this._shellService.unblockUI())
        ).subscribe(([entity]) => {
          this.entity = entity;
          this.createFormValidators();
        })
      } else {
        this._shellService.unblockUI();
        this.createFormValidators();
      }
    });
  }

  private createFormValidators(): void {
    this.formGroup = this.formBuilder.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      identificador: new FormControl(this.entity.identificador, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]),
      nome: new FormControl(this.entity.nome, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]),
      descricao: new FormControl(this.entity.descricao, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]),
      observacao: this.entity.observacao,
      modelo: this.entity.modelo,
      patrimonio: this.entity.patrimonio,
      ativo: [this.entity.ativo],
      dataCriacao: new FormControl({ value: this.entity.dataCriacao, disabled: true }),
      dataInativacao: [this.entity.dataInativacao],
      
      criadoPorId: [this.id ? this.entity.alteradoPor.id : this._credentialsService.authenticatedUser.id],
      criadoPorNome: [this.entity.criadoPor && (this.entity.criadoPor.userName)],
      
      alteradoPorId: [this._credentialsService.authenticatedUser.id],
      alteradoPorNome: [this.entity.alteradoPor && (this.entity.alteradoPor.userName)],
      
      unidadeMedidaId: [this.entity.unidadeMedidaId],
      unidadeMedidaDescricao: [this.entity.unidadeMedida && (this.entity.unidadeMedida.descricao)],
      
      marcaId: [this.entity.marcaId],
      marcaDescricao: [this.entity.marca && (this.entity.marca.descricao)],
      
      tipoInsumoId: [this.entity.tipoInsumoId],
      tipoInsumoDescricao: [this.entity.tipoInsumo && (this.entity.tipoInsumo.descricao), [Validators.required]],

      setorId: [this.entity.setorId],
      setorNome: [this.entity.setor && (this.entity.setor.nome)],

      fornecedorId: [this.entity.fornecedorId],
      fornecedorNome: [this.entity.fornecedor && (this.entity.fornecedor.nome)],
    });
  }

  public save(formModel: Insumo, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._service.update(this.entity).subscribe(
          () => {
            this.goBack();
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.entity).subscribe(
          () => {
            this.goBack();
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: Insumo): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new Insumo(null);
  }
}
