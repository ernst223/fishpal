import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { RoleManagementUsersDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';
import { RoleChangeDialogComponent } from '../administration/role-change-dialog/role-change-dialog.component';

@Component({
  selector: 'app-role-management',
  templateUrl: './role-management.component.html',
  styleUrls: ['./role-management.component.scss']
})
export class RoleManagementComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) pendingSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) pendingPaginator: MatPaginator;

  pendingData: RoleManagementUsersDTO[];

  pendingDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Id', 'Username', 'FullName', 'Facet', 'Role', 'Actions'];
  
  constructor(private snackBar: MatSnackBar, private service: SharedService, public dialog: MatDialog) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service.getRoleManagementUsers().subscribe(a => {
      this.pendingData = a;
      this.pendingDataSource.data = this.pendingData;
    });
  }

  ngAfterViewInit() {
    this.pendingDataSource.sort = this.pendingSort;
    this.pendingDataSource.paginator = this.pendingPaginator;
  }

  applyPendingFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.pendingDataSource.filter = filterValue;
  }

  editRole(id: number) {
    const dialogRef = this.dialog.open(RoleChangeDialogComponent, {
      height: '300px',
      width: '500px',
      data: id
    });

    dialogRef.afterClosed().subscribe(result => {
      this.setupDataStream();
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
