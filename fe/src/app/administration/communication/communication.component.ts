import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';


export interface Card {
  title: string;
  subtitle: string;
  text: string;
}

const DATA: Tile[] = [
  {text: 'One',price:700000,bed:2,bath:1,parking:2},
  {text: 'One',price:700000,bed:2,bath:1,parking:2},{text: 'One',price:700000,bed:2,bath:1,parking:2},
  {text: 'One',price:700000,bed:2,bath:1,parking:2},
  {text: 'One',price:700000,bed:2,bath:1,parking:2},
  {text: 'One',price:700000,bed:2,bath:1,parking:2},
  {text: 'One',price:700000,bed:2,bath:1,parking:2}
];


export interface Tile {
  text: string;
  price: number;
  bed: number;
  bath: number;
  parking: number;
}

@Component({
  selector: 'app-communication',
  templateUrl: './communication.component.html',
  styleUrls: ['./communication.component.scss']
})
export class CommunicationComponent implements OnInit, OnDestroy {
  @ViewChild(MatPaginator, {static: false})
  paginator!: MatPaginator;
  obs!: Observable<any>;
  dataSource: MatTableDataSource<Tile> = new MatTableDataSource<Tile>(DATA);
test:string = 'testering this is a test message to see if the full message doe in fact dispaly or not, but this will have to be seen.';
  constructor(private changeDetectorRef: ChangeDetectorRef,public dialog: MatDialog) { }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.dataSource.paginator = this.paginator;
    this.obs = this.dataSource.connect();
  }

  ngOnDestroy() {
    if (this.dataSource) { 
      this.dataSource.disconnect(); 
    }
  }

  openModal(templateRef) {
    let dialogRef = this.dialog.open(templateRef, {
         width: '550px',
         // data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        // this.animal = result;
    });
}

}
