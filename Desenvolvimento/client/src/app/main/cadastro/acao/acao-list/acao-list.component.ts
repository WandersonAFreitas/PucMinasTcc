import { Component, OnInit } from "@angular/core";
import { BaseListComponent } from "@fuse/components/base-list-component";
import { Acao } from "@fuse/types/models/acao";
import { Router } from "@angular/router";
import { ShellService } from "@fuse/core/shell.service";
import { AcaoService } from "../acao.service";

@Component({
  selector: 'app-acao',
  templateUrl: './acao-list.component.html',
  styleUrls: ['./acao-list.component.css']
})
export class AcaoListComponent extends BaseListComponent<Acao> implements OnInit {

  public displayedColumns: string[] = ['Actions', 'Id', 'Nome'];

  public entityFilter: Acao;

  constructor(
    private _router: Router,
    private _shellService: ShellService,
    private _acaoService: AcaoService
  ) {
    super(_router, _acaoService, _shellService);
    this.entityFilter = this.initFilter();
    this.gridSettingsModel = this.baseInitGridSettings(null, 'Nome', 'asc');
  }

  ngOnInit(): void {
    this.baseLoad(this.gridSettingsModel);
  }

  public create(): void {
    this.router.navigate(['/cadastro/acao/new']);
  }

  public edit(entity: Acao): void {
    this.baseEdit(['/cadastro/acao/edit', entity.id]);
  }

  public delete(entity: Acao): void {
    this.baseDelete({
      titulo: `Confirmar Exclusão`,
      mensagem: `Deseja realmente excluir o Acao: ${entity.nome}?`,
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
    this.baseClear();
  }

  private initFilter(): Acao {
    const model: Acao = new Acao(null);
    return model;
  }
}
