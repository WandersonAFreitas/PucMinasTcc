import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { MatTableDataSource, MatButtonToggleChange } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { ManagerRole } from '@fuse/types/models/manage-user';
import { HttpBaseService } from '@fuse/core/http-base.service';

@Component({
  selector: 'app-manage-user-role',
  templateUrl: './manage-user-role.component.html',
  styleUrls: ['./manage-user-role.component.scss']
})
export class ManageUserRoleComponent implements OnInit {

  @Input() userId: number;
  @Output() dataUserSetor = new EventEmitter();

  dataSource: MatTableDataSource<ManagerRole>;
  displayedColumns: string[] = ['select', 'Id', 'Name'];
  selection = new SelectionModel<ManagerRole>(true, []);

  constructor(private _http: HttpBaseService, ) { }

  ngOnInit() {
    this.getAllRoles();
  }

  private getAllRoles() {
    this._http.get(`/manage/allRoles/${this.userId || 0}`).subscribe((roles: ManagerRole[]) => {
      this.dataSource = new MatTableDataSource<ManagerRole>(roles);
      this.dataUserSetor.emit(this.dataSource.data);
    });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  toggle(row) {
    row.enabled = !this.selection.isSelected(row);
    this.selection.toggle(row);

    this.dataUserSetor.emit(this.dataSource.data);
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
}
