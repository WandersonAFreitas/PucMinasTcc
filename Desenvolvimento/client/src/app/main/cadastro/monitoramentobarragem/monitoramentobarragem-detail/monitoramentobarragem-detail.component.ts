import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSelectionList } from '@angular/material';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { MonitoramentoBarragemService } from '../monitoramentobarragem.service';
import { MonitoramentoBarragem } from '@fuse/types/models/monitoramento-barragem';
import { ShellService } from '@fuse/core/shell.service';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { BarragemService } from '../../barragem/barragem.service';
import { UnidadeMedidaService } from '../../unidademedida/unidademedida.service';
import { NivelMonitoramentoService } from '../../nivelmonitoramento/nivelmonitoramento.service';
import { SensorService } from '../../sensor/sensor.service';
import { ConsultoriaService } from '../../consultoria/consultoria.service';
import { TipoMonitoramentoService } from '../../tipomonitoramento/tipomonitoramento.service';

@Component({
  templateUrl: './monitoramentobarragem-detail.component.html',
  styleUrls: ['./monitoramentobarragem-detail.component.css']
})
export class MonitoramentoBarragemDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  private entity: MonitoramentoBarragem;

  public gridSettingsModel: GridSettings;
  public gridSettingsDescricaoModel: GridSettings;

  constructor(
    private location: Location,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: MonitoramentoBarragemService,
    private _shellService: ShellService,
    public  _barragemService: BarragemService,
    public  _unidadeMedidaService: UnidadeMedidaService,
    public  _nivelMonitoramentoService: NivelMonitoramentoService,
    public  _sensorService: SensorService,
    public  _consultoriaService: ConsultoriaService,
    public  _tipoMonitoramentoService: TipoMonitoramentoService,

  ) {
    this.initialize();
    this.createFormValidators();
  }

  ngOnInit() {
    const rule = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [rule]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
    
    const ruleDescricao = new Rule('Descricao', 'cn', null);
    const filterDescricao = new Filter(1, [ruleDescricao]);
    this.gridSettingsDescricaoModel = new GridSettings(true, 1, 10, 'Descricao', 'asc', filterDescricao);
    
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
      descricao: new FormControl(this.entity.descricao, [Validators.required, Validators.minLength(1), Validators.maxLength(300)]),
      observacao: new FormControl(this.entity.descricao, [Validators.minLength(1), Validators.maxLength(300)]),
      
      barragemId: [this.entity.barragemId],
      barragemNome: [this.entity.barragem && (this.entity.barragem.nome)],
      
      nivelMonitoramentoId: new FormControl(this.entity.nivelMonitoramentoId, [Validators.required]),
      nivelMonitoramentoDescricao: [this.entity.nivelMonitoramento && (this.entity.nivelMonitoramento.descricao)],
      
      nivel: [this.entity.nivel],
      unidadeMedidaId:  new FormControl(this.entity.unidadeMedidaId, [Validators.required]),
      unidadeMedidaDescricao: [this.entity.unidadeMedida && (this.entity.unidadeMedida.descricao)],
    
      dataHora: new FormControl(this.entity.dataHora, [Validators.required]),
      latitude: [this.entity.latitude],
      longitude: [this.entity.longitude],

      sensorId: [this.entity.sensorId],
      sensorDescricao: [this.entity.sensor && (this.entity.sensor.descricao)],
      
      consultoriaId: [this.entity.consultoriaId],
      consultoriaNome: [this.entity.consultoria && (this.entity.consultoria.nome)],

      tipoMonitoramentoId: new FormControl(this.entity.tipoMonitoramentoId, [Validators.required]),
      tipoMonitoramentoNome: [this.entity.tipoMonitoramento && (this.entity.tipoMonitoramento.nome)],
    });
  }

  public save(formModel: MonitoramentoBarragem, isValid: boolean): void {
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

  private prepareToSave(formModel: MonitoramentoBarragem): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this.location.back();
  }

  private initialize(): void {
    this.entity = new MonitoramentoBarragem(null);
  }
}
