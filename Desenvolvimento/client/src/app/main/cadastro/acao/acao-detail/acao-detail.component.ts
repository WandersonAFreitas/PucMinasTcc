import { Component, OnInit, ViewChild } from "@angular/core";
import { MatSelectionList } from "@angular/material";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { Acao } from "@fuse/types/models/acao";
import { ActivatedRoute, Router } from "@angular/router";
import { AcaoService } from "../acao.service";
import { ShellService } from "@fuse/core/shell.service";
import { forkJoin } from "rxjs";
import { finalize } from "rxjs/operators";

@Component({
  templateUrl: './acao-detail.component.html',
  styleUrls: ['./acao-detail.component.css']
})
export class AcaoDetailComponent implements OnInit {

  @ViewChild(MatSelectionList) perfilList: MatSelectionList;

  public id: number;
  public formGroup: FormGroup;

  private entity: Acao;

  constructor(
    private _router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _service: AcaoService,
    private _shellService: ShellService
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
      nome: new FormControl(this.entity.nome, [Validators.required, Validators.minLength(3), Validators.maxLength(300)]),
    });
  }

  public save(formModel: Acao, isValid: boolean): void {
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

  private prepareToSave(formModel: Acao): void {
    this.entity = { ...this.entity, ...formModel };
  }

  public goBack(): void {
    this._router.navigate(['cadastro/acao']);
  }

  private initialize(): void {
    this.entity = new Acao(null);
  }
}
