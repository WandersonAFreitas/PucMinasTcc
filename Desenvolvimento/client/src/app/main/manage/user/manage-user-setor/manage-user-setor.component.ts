import { Component, OnInit, Input, Output, EventEmitter, ɵConsole } from '@angular/core';
import { MatTableDataSource, MatSort } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { ManageUserSetor } from '@fuse/types/models/manage-user-setor';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Component({
  selector: 'app-manage-user-setor',
  templateUrl: './manage-user-setor.component.html',
  styleUrls: ['./manage-user-setor.component.scss']
})
export class ManageUserSetorComponent implements OnInit {

  @Input() userId: number;
  @Output() dataUserSetor = new EventEmitter();

  displayedColumns: string[] = ['select', 'nome'];
  dataSource = new MatTableDataSource<ManageUserSetor>();
  selection = new SelectionModel<ManageUserSetor>(true, []);

  constructor(private _http: HttpBaseService) { }

  ngOnInit() {
    this.getAllSetor();
  }

  private getAllSetor() {
    this._http.get(`/manage/allSetor/${this.userId || 0}`).subscribe((setore: ManageUserSetor[]) => {
      this.dataSource = new MatTableDataSource<ManageUserSetor>(setore);
      this.dataUserSetor.emit(this.dataSource.data);
    });
  }

  isAllSelected() {

    this.dataUserSetor.emit(this.dataSource.data);

    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {

    if (this.isAllSelected()) {
      this.selection.clear();

      this.dataSource.data.forEach(row => {
        row.enabled = false;
      });
    } else {
      this.dataSource.data.forEach(row => {
        row.enabled = true;
        this.selection.select(row)
      });
    }

    this.dataUserSetor.emit(this.dataSource.data);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  toggle(row) {
    row.enabled = !this.selection.isSelected(row);
    this.selection.toggle(row);

    this.selectNiveis(row);

    this.dataUserSetor.emit(this.dataSource.data);
  }

  selectNiveis(row) {

    this.dataSource.data.forEach(x => {

      if (this.selection.isSelected(row)) {

        if (x.setorId == row.setorPaiId) {
          x.enabled = true;
          this.selection.select(x);

          if (x.setorPaiId != null) {
          this.selectNiveis(x);
          }
        }
      } else {
        if (x.setorPaiId == row.setorId) {
          x.enabled = false;
          this.selection.deselect(x);
          this.selectNiveis(x);
        }
      }
    });
  }
}
