import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FluxoItemAnexoService } from '../fluxo-item-anexo.service';
import { FluxoTipoAnexoService } from '../../../../fluxo-tipo-anexo/fluxo-tipo-anexo.service';
import { FluxoItemAnexo } from '@fuse/types/models/fluxo-item-anexo';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-fluxo-item-anexo-detail',
  templateUrl: './fluxo-item-anexo-detail.component.html',
  styleUrls: ['./fluxo-item-anexo-detail.component.scss']
})
export class FluxoItemAnexoDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoItemAnexo: FluxoItemAnexo;
  private fluxoId: number;
  private fluxoItemId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoItemAnexoDetailComponent>,
    private _service: FluxoItemAnexoService,
    public _fluxoTipoAnexoService: FluxoTipoAnexoService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoItemAnexo: FluxoItemAnexo, fluxoId: number, fluxoItemId: number }
  ) {
    this.fluxoItemAnexo = new FluxoItemAnexo(null);
    this.fluxoId = this.data.fluxoId;
    this.fluxoItemId = this.data.fluxoItemId;
    this.id = this.data.fluxoItemAnexo && this.data.fluxoItemAnexo.id;


    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoItemAnexo = this.data.fluxoItemAnexo;

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome], [new Filter(1, [new Rule('FluxoId', 'eq', this.fluxoId.toString())])]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    if (this.fluxoItemAnexo) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.fluxoItemAnexo.id, disabled: true }],
      fluxoItemId: [this.fluxoItemId],
      obrigatorio: [this.fluxoItemAnexo.obrigatorio],
      exigeAssinaturaDigital: [this.fluxoItemAnexo.exigeAssinaturaDigital],
      fluxoTipoAnexoId: [this.fluxoItemAnexo.fluxoTipoAnexoId],
      fluxoTipoAnexoNome: [this.fluxoItemAnexo.fluxoTipoAnexo && (this.fluxoItemAnexo.fluxoTipoAnexo.nome)]
    });
  }

  public save(formModel: FluxoItemAnexo, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoItemAnexo.id) {
        this._service.update(this.fluxoItemAnexo).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoItemAnexo).subscribe(
          (fluxoSituacao) => {
            this.id = fluxoSituacao.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoItemAnexo): void {
    this.fluxoItemAnexo = { ...this.fluxoItemAnexo, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }
}
