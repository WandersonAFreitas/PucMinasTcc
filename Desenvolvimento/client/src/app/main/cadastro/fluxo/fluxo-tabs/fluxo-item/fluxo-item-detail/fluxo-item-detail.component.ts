import { Component, OnInit, EventEmitter, Output, ViewChild, Input, OnChanges } from '@angular/core';
import { MatSelectionList } from '@angular/material';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { FluxoSituacaoService } from '../../fluxo-situacao/fluxo-situacao.service';
import { FluxoItemService } from '../fluxo-item.service';
import { FluxoAcaoService } from '../../fluxo-acao/fluxo-acao.service';
import { forkJoin } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { FluxoItem } from '@fuse/types/models/fluxo-item';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';
import { TipoSituacaoEnum } from '@fuse/types/models/enums/tipo-situacao-enum';

@Component({
  selector: 'app-fluxo-item-detail',
  templateUrl: './fluxo-item-detail.component.html',
  styleUrls: ['./fluxo-item-detail.component.scss']
})
export class FluxoItemDetailComponent implements OnInit {

  @Input() fluxoId: number;
  @Output() tabItem = new EventEmitter();
  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public formGroupItem: FormGroup;
  private entity: FluxoItem;
  public fluxoItemId: number;
  public gridSettingsModel: GridSettings;
  public gridSettingsModelSitInicial: GridSettings;
  public gridSettingsModelSitFinal: GridSettings;

  constructor(
    private formBuilder: FormBuilder,
    public _fluxoItem: FluxoItemService,
    public _fluxoSituacao: FluxoSituacaoService,
    public _fluxoAcao: FluxoAcaoService,
    private _shellService: ShellService
  ) {
    this.entity = new FluxoItem(null);
    this.createFormValidators();
  }

  ngOnInit() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome], [new Filter(1, [new Rule('FluxoId', 'eq', this.fluxoId.toString())])]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    const ruleFluxo = new Rule('FluxoId', 'eq', this.fluxoId.toString());
    const ruleTipoSitTodos = new Rule('TipoSituacao', 'eq', '' + TipoSituacaoEnum.Todas);

    const ruleTipoSitInicial = new Rule('TipoSituacao', 'eq', '' + TipoSituacaoEnum.SituacaoAtual);
    const filterSitInicial = new Filter(0, [ruleNome], [new Filter(0, [ruleFluxo]), new Filter(1, [ruleTipoSitInicial, ruleTipoSitTodos])]);
    this.gridSettingsModelSitInicial = new GridSettings(true, 1, 10, 'Nome', 'asc', filterSitInicial);

    const ruleTipoSitProx = new Rule('TipoSituacao', 'eq', '' + TipoSituacaoEnum.ProximaSituacao);
    const ruleTipoSitFinal = new Rule('TipoSituacao', 'eq', '' + TipoSituacaoEnum.SituacaoFinal);
    const filterSitFinal = new Filter(0, [ruleNome], [new Filter(0, [ruleFluxo]), new Filter(1, [ruleTipoSitProx, ruleTipoSitFinal, ruleTipoSitTodos])]);
    this.gridSettingsModelSitFinal = new GridSettings(true, 1, 10, 'Nome', 'asc', filterSitFinal);

    this.fluxoItemId = FluxoItemService.fluxoItemId;

    if (this.fluxoItemId) {
      forkJoin(
        this._fluxoItem.get(this.fluxoItemId, false),
      ).pipe(
        finalize(() => this._shellService.unblockUI())
      ).subscribe(([entity]) => {
        this.entity = entity;
        this.createFormValidators();
        FluxoItemService.fluxoItemId = null;
      })
    } else {
      this._shellService.unblockUI();
      this.createFormValidators();
      FluxoItemService.fluxoItemId = null;
    }
  }

  private createFormValidators(): void {
    this.formGroupItem = this.formBuilder.group({
      id: new FormControl({ value: this.entity.id, disabled: true }),
      situacaoAtualId: [this.entity.situacaoAtualId],
      situacaoAtualNome: [this.entity.situacaoAtual && (this.entity.situacaoAtual.nome)],
      acaoId: [this.entity.acaoId],
      acaoNome: [this.entity.acao && (this.entity.acao.nome)],
      proximaSituacaoId: [this.entity.proximaSituacaoId],
      proximaSituacaoNome: [this.entity.proximaSituacao && (this.entity.proximaSituacao.nome)]
    });
  }

  public saveItem(formModel: FluxoItem, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.entity.id) {
        this._fluxoItem.update(this.entity).subscribe(
          () => {
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
        } else {
          this._fluxoItem.save(this.entity).subscribe(
            (data) => {
              this.fluxoItemId = data.id;
              this.entity.id = data.id;
              this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoItem): void {
    if (!formModel.fluxoId) {
      formModel.fluxoId = this.fluxoId;
    }

    this.entity = { ...this.entity, ...formModel };
  }

  onTabItem() {
    this.tabItem.emit(false);
  }
}
