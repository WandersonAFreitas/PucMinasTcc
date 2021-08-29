import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { AssuntoService } from '@fuse/core/assunto.service';
import { EmpresaService } from 'app/main/cadastro/empresa/empresa.service';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-fluxo-assunto-detail',
  templateUrl: './fluxo-assunto-detail.component.html',
  styleUrls: ['./fluxo-assunto-detail.component.scss']
})
export class FluxoAssuntoDetailComponent implements OnInit {

  public formGroup: FormGroup;
  private fluxoId: number;
  public id: number;

  public gridSettingsModel: GridSettings;
  public gridSettingsModelEmpresa: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoAssuntoDetailComponent>,
    public _service: AssuntoService,
    public _serviceEmpresa: EmpresaService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoId: number }
    ) {
      this.fluxoId = this.data.fluxoId;

      const ruleNome = new Rule('Nome', 'cn', null);
      this.gridSettingsModelEmpresa = new GridSettings(true, 1, 10, 'Nome', 'asc', new Filter(1, [ruleNome]));
  }

  ngOnInit() {
    this.createFormValidators();
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      empresaId: [],
      empresaNome: [],
      assuntoId: [],
      assuntoNome: [{value: '', disabled: true}],
    });
  }

  public save(formModel: any, isValid: boolean): void {
    if (isValid) {
      this._service.get(formModel.assuntoId).subscribe(data => {
        data.fluxoId = this.fluxoId;
        this._service.update(data).subscribe(() => {
          this.thisDialogRef.close('Ok');
          this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
        });
      });
    }
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }

  private gridSettingsAssunto() {
    const ruleNome = new Rule('Nome', 'cn', null);
    const ruleFluxo = new Rule('FluxoId', 'nu', '0');
    const ruleEmpresa = new Rule('EmpresaId', 'eq', this.formGroup.controls.empresaId.value);

    const filter = new Filter(1, [ruleNome], [new Filter(0, [ruleFluxo, ruleEmpresa])]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);
  }

  public reciverEventChange() {
    this.gridSettingsAssunto();
    this.setEnableDisableAssunto();
  }

  public reciverEventClear() {
    this.formGroup.controls.assuntoId.setValue(null);
    this.formGroup.controls.assuntoNome.setValue(null);

    this.gridSettingsAssunto();
    this.setEnableDisableAssunto();
  }

  private setEnableDisableAssunto() {
    if (this.formGroup.controls.empresaId.value == null) {
      this.formGroup.controls.assuntoNome.disable();
    } else {
      this.formGroup.controls.assuntoNome.enable();
    }
  }
}
