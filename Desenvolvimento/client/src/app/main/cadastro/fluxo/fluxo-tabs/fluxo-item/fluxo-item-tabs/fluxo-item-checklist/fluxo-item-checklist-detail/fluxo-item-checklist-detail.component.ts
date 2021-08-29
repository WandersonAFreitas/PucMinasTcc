import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FluxoItemChecklistService } from '../fluxo-item-checklist.service';
import { FluxoItemCheckList } from '@fuse/types/models/fluxo-item-checklist';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { ShellService } from '@fuse/core/shell.service';

@Component({
  selector: 'app-fluxo-item-checklist-detail',
  templateUrl: './fluxo-item-checklist-detail.component.html',
  styleUrls: ['./fluxo-item-checklist-detail.component.scss']
})
export class FluxoItemChecklistDetailComponent implements OnInit {

  public id: number;
  public formGroup: FormGroup;

  private fluxoItemCheckList: FluxoItemCheckList;
  private fluxoItemId: number;
  public unlock: boolean;

  public gridSettingsModel: GridSettings;

  constructor(
    public thisDialogRef: MatDialogRef<FluxoItemChecklistDetailComponent>,
    private _service: FluxoItemChecklistService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { fluxoItemCheckList: FluxoItemCheckList, fluxoItemId: number }
  ) {
    this.fluxoItemCheckList = new FluxoItemCheckList(null);
    this.fluxoItemId = this.data.fluxoItemId;
    this.id = this.data.fluxoItemCheckList && this.data.fluxoItemCheckList.id;

    const ruleNome = new Rule('Nome', 'cn', null);
    const filter = new Filter(1, [ruleNome]);
    this.gridSettingsModel = new GridSettings(true, 1, 10, 'Nome', 'asc', filter);

    this.createFormValidators();
  }

  ngOnInit() {
    this.fluxoItemCheckList = this.data.fluxoItemCheckList;

    if (this.fluxoItemCheckList) {
      this.createFormValidators();
    }
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      id: [{ value: this.fluxoItemCheckList.id, disabled: true }],
      nome: [this.fluxoItemCheckList.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      fluxoItemId: [this.fluxoItemId]
    });
  }

  public save(formModel: FluxoItemCheckList, isValid: boolean): void {
    if (isValid) {
      this.prepareToSave(formModel);
      if (this.fluxoItemCheckList.id) {
        this._service.update(this.fluxoItemCheckList).subscribe(
          () => {
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Atualizado com sucesso!'], timeout: 3000 });
          });
      } else {
        this._service.save(this.fluxoItemCheckList).subscribe(
          (fluxoSituacao) => {
            this.id = fluxoSituacao.id;
            this.thisDialogRef.close('Ok');
            this._shellService.alert().success({ messages: ['Salvo com sucesso!'], timeout: 3000 });
          });
      }
    }
  }

  private prepareToSave(formModel: FluxoItemCheckList): void {
    this.fluxoItemCheckList = { ...this.fluxoItemCheckList, ...formModel };
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }
}
