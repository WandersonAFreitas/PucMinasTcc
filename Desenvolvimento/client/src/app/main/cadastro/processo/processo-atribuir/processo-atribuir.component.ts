import { Component, OnInit, ViewChild, Inject, ElementRef, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, } from '@angular/forms';
import { MatSelectionList, MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent, MatTableDataSource } from '@angular/material';
import { ProcessoService } from '../processo.service';
import { User } from '@fuse/types/models/user';
import { Processo } from '@fuse/types/models/processo';
import { UserService } from '@fuse/core/user.service';
import { ShellService } from '@fuse/core/shell.service';
import { Rule, Filter, GridSettings } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'proceso-atribuir',
  templateUrl: './processo-atribuir.component.html',
  styleUrls: ['./processo-atribuir.component.css']
})
export class ProcessoAtribuirComponent implements OnInit, AfterViewInit {

  public processoId: number;
  public formGroup: FormGroup;

  public cmbResposaveis = new Array<User>();

  private processo: Processo;

  constructor(
    public thisDialogRef: MatDialogRef<ProcessoAtribuirComponent>,
    private _processoService: ProcessoService,
    private _userService: UserService,
    private _shellService: ShellService,
    private _fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { processo: Processo }
  ) {
    this.processoId = this.data.processo && this.data.processo.id;
    this.createFormValidators();
  }

  ngOnInit() {
    this.processo = this.data.processo;
    this.createFormValidators();
    this.initGridSettingsModelResponsavel();
  }

  ngAfterViewInit() {
  }

  private createFormValidators(): void {
    this.formGroup = this._fb.group({
      // responsavelNome: [this.processo && this.processo.responsavel && this.processo.responsavel.userName, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]],
      responsavelId: [this.processo && this.processo.responsavel && this.processo.responsavelId, [Validators.required]],
    });
  }

  public initGridSettingsModelResponsavel() {
    const ruleNome = new Rule('UserName', 'cn', null);
    const filter = new Filter(0, [ruleNome]);

    const gridSettingsModel = new GridSettings(true, 1, 10, 'UserName', 'asc', filter);

    this._userService.getByFilter(gridSettingsModel).subscribe(page => {
      this.cmbResposaveis = page.content;
    });
  }

  public save(formModel: any, isValid: boolean): void {
    if (isValid) {
      const responsavelId: number = this.formGroup.controls.responsavelId.value;
      if (this.processo && this.processo.id) {
        this._processoService.setResponsavel(this.processoId, responsavelId).subscribe(
          (responsavel) => {
            this.thisDialogRef.close(responsavel);
            this._shellService.alert().success({ messages: ['Responsável atribuído com sucesso!'], timeout: 3000 });
          });
      } else {
        this.onCloseCancel();
      }
    }
  }

  public onCloseCancel() {
    this.thisDialogRef.close('Cancel');
  }
}
