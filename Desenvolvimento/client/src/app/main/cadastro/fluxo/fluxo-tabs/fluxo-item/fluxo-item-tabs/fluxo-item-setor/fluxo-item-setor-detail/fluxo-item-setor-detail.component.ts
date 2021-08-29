import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FluxoItemSetorService } from '../../fluxo-item-setor.service';
import { FluxoItemSetor } from '@fuse/types/models/fluxo-item-setor';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { SetorService } from 'app/main/cadastro/empresa/setor.service';
import { EmpresaService } from 'app/main/cadastro/empresa/empresa.service';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-fluxo-item-setor-detail',
  templateUrl: './fluxo-item-setor-detail.component.html',
  styleUrls: ['./fluxo-item-setor-detail.component.scss']
})
export class FluxoItemSetorDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoItemSetor: FluxoItemSetor;
  private fluxoId: number;
  private fluxoItemId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;
  public gridSettingsModelEmpresa: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoItemSetorDetailComponent>,
    private _service: FluxoItemSetorService,
    public _setorService: SetorService,
    public _serviceEmpresa: EmpresaService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoItemSetor: FluxoItemSetor, fluxoId: number, fluxoItemId: number }
  ) {
    this.fluxoItemSetor = new FluxoItemSetor(null);
    this.fluxoId = this.data.fluxoId;
    this.fluxoItemId = this.data.fluxoItemId;
    this.id = this.data.fluxoItemSetor && this.data.fluxoItemSetor.id;

    const ruleNome = new Rule('Nome', 'cn', null);
    this.gridSettingsModelEmpresa = new GridSettings(true, 1, 10, 'Nome', 'asc', new Filter(1, [ruleNome]));

    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoItemSetor = this.data.fluxoItemSetor;

    if (this.fluxoItemSetor) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.fluxoItemSetor.id, disabled: true }],
      fluxoItemId: [this.fluxoItemId],
      empresaId: [],
      empresaNome: [],
      setorId: [this.fluxoItemSetor.setorId],
      setorNome: [this.fluxoItemSetor.setor && (this.fluxoItemSetor.setor.nome)]
    });
  }

  public save(formModel: FluxoItemSetor, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoItemSetor.id) {
        this._service.update(this.fluxoItemSetor).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoItemSetor).subscribe(
          (fluxoSituacao) => {
            this.id = fluxoSituacao.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoItemSetor): void {
    this.fluxoItemSetor = { ...this.fluxoItemSetor, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  private gridSettingsSetor() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleFluxo = new Rule('FluxoId', 'nu', '0');
    const ruleEmpresa = new Rule('EmpresaId', 'eq', this.formGroup.controls.empresaId.value);

    const filter = new Filter(1, [ruleNome], [new Filter(0, [ruleFluxo, ruleEmpresa])]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  public reciverEventChange() {
    this.gridSettingsSetor();
    this.setEnableDisableAssunto();
  }

  public reciverEventClear() {
    this.formGroup.controls.setorId.setValue(null);
    this.formGroup.controls.setorNome.setValue(null);

    this.gridSettingsSetor();
    this.setEnableDisableAssunto();
  }

  private setEnableDisableAssunto() {
    if (this.formGroup.controls.empresaId.value == null) {
      this.formGroup.controls.setorNome.disable();
    } else {
      this.formGroup.controls.setorNome.enable();
    }
  }
}
