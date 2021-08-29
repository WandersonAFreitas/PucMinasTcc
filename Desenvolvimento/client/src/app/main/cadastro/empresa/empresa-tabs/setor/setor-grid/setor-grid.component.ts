import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input, ViewChild, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { MatDialog, MatAutocompleteSelectedEvent, MatTable } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { SetorService } from '../../../setor.service';
import { EmpresaService } from '../../../empresa.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Empresa } from '@fuse/types/models/empresa';
import { Setor } from '@fuse/types/models/setor';
import { ShellService } from '@fuse/core/shell.service';
import { AddNewRowComponent } from '@fuse/components/custom-mat-table/add-new-row/add-new-row.component';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'empresa-tab-setor-grid',
  templateUrl: './setor-grid.component.html',
  styleUrls: ['./setor-grid.component.css'],
  animations: [
    ...fuseAnimations,
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class EmpresaSetorGridComponent implements OnInit {

  @ViewChildren(MatTable) matTables: QueryList<MatTable<any>>;
  @ViewChild('table') table1: MatTable<any>;
  @Input() public empresa: Empresa;
  @Input() public setores: Setor[];

  @Input()
  get add(): boolean { return this._add; }
  set add(add: boolean) {
    this._add = coerceBooleanProperty(add);
  }
  _add: boolean;

  public statusFilter: Array<any> = [
    { key: true, value: 'Sim' },
    { key: false, value: 'Não' }
  ];

  public empresaId: number;
  public displayedColumns: string[] = ['Action', 'Nome', 'Sigla', 'Ativo'];

  constructor(
    private route: ActivatedRoute,
    private _dialog: MatDialog,
    private _router: Router,
    public _setorService: SetorService,
    public _empresaService: EmpresaService,
    private _shellService: ShellService,
  ) {
    this.empresaId = this.route.params['value'].id || 0;
  }

  ngOnInit(): void {
  }


  public getRows(table: MatTable<any>, data: Array<any>) {
    if (data) {
      if (table.dataSource && (table.dataSource['length'] === data.length)) {
        let rows = [];
        data.forEach((element: any) => rows.push(element, { detailRow: true, element }));
        rows = rows.sort(this.sortTable())
        table.dataSource = rows;
      } else if (!table.dataSource && table['_headerRowDefs'] && data.length) {
        data = data.sort(this.sortTable())
        table.dataSource = data;
      }
    }
  }

  public sortTable() {
    return (a, b) => a.id < b.id ? -1 : (a.id > b.id ? 1 : 0);
  }

  public isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');

  public toggleRow(table: MatTable<any>, index: number, event: MouseEvent, tableIndex: number) {
    if (event.target['tagName'] === 'MAT-CELL') {
      table.dataSource[index + 1]['element']['show_' + index + '_' + tableIndex] = !table.dataSource[index + 1]['element']['show_' + index + '_' + tableIndex];
    }
  }

  public closeAll() {
    this._empresaService.get(this.empresaId, false).subscribe(empresa => {
      this.empresa = empresa;
      this.empresa.setores = this.empresa.setores.filter(x => !x.setorPaiId);
      this.matTables.map(x => x.dataSource = null);
    });
  }

  public openAll() {
    this.matTables.map((table, tableIndex) => {
      const data = <Array<any>>table.dataSource;
      if (data) {
        const rowsElementShow = data.filter(x => x.hasOwnProperty('detailRow'));
        rowsElementShow.map((x, index) => {
          table.dataSource[index + 1] && table.dataSource[index + 1]['element'] && (table.dataSource[index + 1]['element']['show_' + index + '_' + (tableIndex + 1)] = true);
        });
      }
    });
  }

  public adicionaNovoSetor(setorPai: Setor): void {
    const dialogRef = this._dialog.open(AddNewRowComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        empresa: this.empresa,
        setorPai
      },
      // disableClose: true
    });
    this.afterClosedRefreshGrids(dialogRef, setorPai && setorPai.id);
  }

  public alterarSetor(setorNovo: Setor): void {
    const dialogRef = this._dialog.open(AddNewRowComponent, {
      panelClass: 'event-form-dialog',
      width: '800px',
      data: {
        setorNovo,
        empresa: this.empresa,
      },
      // disableClose: true
    });
    this.afterClosedRefreshGrids(dialogRef, setorNovo.id);
  }

  public removeSetor(setor: Setor): void {
    this._shellService.confirm().confirm({ message: `Deseja realmente remover: ${setor.nome}?`, title: `Confirmar remoção de setor` })
      .subscribe(res => {
        if (res) {
          this._setorService.remove(setor.id).subscribe(() => {
            this.refreshGrids(setor.setorPaiId, setor.setorPaiId ? 'delete' : setor.id);
            this._shellService.alert().success({ messages: [`Setor "${setor.nome}" foi removido com sucesso!`], timeout: 5000 });
          });
        }
      });
  }

  private afterClosedRefreshGrids(dialogRef: any, setorId: number) {
    dialogRef.afterClosed().subscribe((result: any) => {
      if (!!result && result != 'Cancel') {
        this.refreshGrids(setorId || result);
      }
    });
  }

  private refreshGrids(setorId: number, deleteCheck: any = null) {
    if (!setorId && deleteCheck) {
      let data = <Array<any>>this.table1.dataSource;
      data = data.filter(x => x.id != deleteCheck);
      if (data.length == 1 && data[0].hasOwnProperty('detailRow')) {
        data = new Array<any>();
      }
      this.table1.dataSource = data;
    } else {
      this._setorService.get(setorId, false).subscribe(setor => {
        this.empresa = setor.empresa;
        this.setores = setor.setorPaiId ? setor.setorPai.setoresFilhos.concat(setor) : this.empresa.setores.concat(setor).filter(x => !x.setorPaiId);
        this.setores = this.setores.sort(this.sortTable())
        let data = <Array<any>>this.table1.dataSource;
        if (deleteCheck === 'delete') {
          const setorPai = this.setores.find(x => x.id === setorId);
          const setoresFilhosIds = setorPai.setoresFilhos.map(x => x.id);
          data = data.filter(x => !!~setoresFilhosIds.indexOf(x.id) || (x.element && !!~setoresFilhosIds.indexOf(x.element.id)));
          this.table1.dataSource = data;
        } else if (data) {
          const rowsElementShow = data.filter(x => x.hasOwnProperty('detailRow')).map(x => x.element).filter(x => Object.keys(x).find(y => !!~y.indexOf('show_')));
          const toggleRows = this.checkAll(rowsElementShow);
          this.checkEntity(this.setores, toggleRows);
          const rows = [];
          this.setores.map((element: any) => rows.push(element, { detailRow: true, element }));
          this.table1.dataSource = rows;
        }
      });
    }
  }

  private checkEntity(setores: Setor[], toggleRows: Array<any>) {
    setores.filter(x => {
      const test = toggleRows.find(y => y.id == x.id);
      if (test) {
        x[test.show] = true;
      }
      if (x.setoresFilhos) {
        this.checkEntity(x.setoresFilhos, toggleRows);
      }
    });
  }

  private checkAll(array: Array<any>): Array<any> {
    const newArray = new Array<any>();
    array.filter(obj => {
      Object.keys(obj).filter(key => {
        this.checkEach(obj, key, newArray);
      });
    })
    return newArray;
  }

  private checkEach(obj: any, key: string, newArray: Array<any>) {
    if (!!~key.indexOf('show_') && obj[key]) {
      newArray.push({ id: obj['id'], show: key });
    } else if (key === 'setoresFilhos') {
      obj[key].filter(objKey => {
        Object.keys(objKey).filter(key2 => {
          this.checkEach(objKey, key2, newArray);
        });
      });
    }
  }
}
