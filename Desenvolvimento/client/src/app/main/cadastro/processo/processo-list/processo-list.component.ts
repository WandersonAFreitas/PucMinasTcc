import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnChanges } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ProcessoService } from '../processo.service';
import { Subscription } from 'rxjs';
import { BaseListComponent } from '@fuse/components/base-list-component';
import { Processo } from '@fuse/types/models/processo';
import { ShellService } from '@fuse/core/shell.service';
import { CredentialsService } from '@fuse/core/credentials.service';
import { User } from '@fuse/types/models/user';
import { Filter, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';

@Component({
  selector: 'app-processo',
  templateUrl: './processo-list.component.html',
  styleUrls: ['./processo-list.component.css']
})
export class ProcessoListComponent extends BaseListComponent<Processo> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Empresa.Nome', 'Assunto.Nome', 'Sequencial', 'Ano', 'Situacao.Nome'];

  public entityFilter: Processo;
  private path: string;
  private inscricao: Subscription;

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _shellService: ShellService,
    private _processoService: ProcessoService,
    private _credentialsService: CredentialsService,
  ) {
    super(_router, _processoService, _shellService);

    this.inscricao = this._route.url.subscribe(
      (url) => {
        if (url.length > 0) {
          this.path = url[0].path;
        }
      }
    );
  }

  ngOnInit() {
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(this.initialFilter(), 'Nome', 'asc');
    this.baseLoad(this.gridSettingsModel);
  }

  ngOnDestroy() {
    this.inscricao.unsubscribe();
  }

  private initFilter(): Processo {
    const model: Processo = new Processo(null);
    model.responsavel = new User(null);
    return model;
  }

  private initialFilter(): Filter {
    let rules = [];

    if (this.path == 'meusprocessos') {
      rules = [new Rule('responsavel.userName', this.op.cn, '' + this._credentialsService.authenticatedUser.userName)];
    }
    else if (this.path == 'naoatribuido') {
      rules = [new Rule('responsavelId', this.op.nu, '0')];
         }

    const initialFilter = new Filter(this.group.and, rules);
    return initialFilter;
  }

  public create(): void {
    this.router.navigate(['/cadastro/processo/new']);
  }

  public edit(entity: Processo): void {
    this.baseEdit(['/cadastro/processo/edit', entity.id]);
  }

  public delete(entity: Processo): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Processo: ${entity.sequencial}?`,
      callback: () => {
        this.service.remove(entity.id).subscribe(() => {
          if (this.pagination.content.length === 1) {
            this.baseSetToFistPageGridSettings();
          }
          this.baseSearch(this.gridSettingsModel);
        });
      }
    });
  }

  public search(): void {
    this.baseSearch(this.gridSettingsModel);
  }

  public clear() {
    this.entityFilter = this.initFilter();
    this.baseClear(this.initialFilter());
  }
}
