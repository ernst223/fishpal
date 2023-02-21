import { AfterViewInit, Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { InsertClothesOrderModel } from '../models/add-clothes-order-form-model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { AdministrationService } from '../administration.service';

interface itemValue {
  value: string;
}
export interface UserData {
  id: string;
  name: string;
  category: string;
  price: string;
}

const NAMES: string[] = [
  'Tshirt',
  'Long Short',
  'Rip stop jacket',
  'Hoody',
  'Beany',
  'Shoes',
  'pants',
  'formal shirt',
  'socks',
  'keepnet',
  'flag',
  'bag',
  'rod sock',
  'wimpel',
  'glasses',
  'stickers',
  'cups',
  'mugs',
  'glas',
];


@Component({
  selector: 'app-clothes-items',
  templateUrl: './clothes-items.component.html',
  styleUrls: ['./clothes-items.component.scss']
})
export class ClothesItemsComponent  implements OnInit, AfterViewInit {
  rowToUpdate:InsertClothesOrderModel = {} as any;
  addUpdateItem:boolean = true;
  clothesItemsModal: InsertClothesOrderModel = {} as any
  addItemForm!: FormGroup;
  category: itemValue[] = [
    { value: 'Battle dress' },
    { value: 'Formals' },
    { value: 'Other' },
  ];

  displayedColumns: string[] = ['id', 'name', 'category', 'price', 'actions'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(public snackBar: MatSnackBar, private adminService: AdministrationService,private formBuilder: FormBuilder, public dialog: MatDialog) {}

  ngAfterViewInit() {

  }

  getRecord(name)
  {
    this.addUpdateItem = false;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngOnInit(): void {
    this.createForm();
    this.getAllItems();
  }

  getAllItems(){
    this.adminService.getOrderItem().subscribe(result => {
      console.log("result of item get",result);
      this.dataSource = new MatTableDataSource(result);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

  createForm(): void {
    this.addItemForm = this.formBuilder.group({
      the_Category: ['', Validators.required],
      the_ItemName: ['', Validators.required],
      the_Price: ['', Validators.required]
    });
  }

  onAddItem() {
    if (this.addItemForm.invalid) {
      this.validateAllFormFields(this.addItemForm);
      return;
    }

    const formData = this.addItemForm.getRawValue();

    this.clothesItemsModal.categoryName = formData.the_Category;
    this.clothesItemsModal.itemName = formData.the_ItemName;
    this.clothesItemsModal.itemCost = formData.the_Price

    this.adminService.insertOrderItem(this.clothesItemsModal).subscribe(result => {
      this.openSnackBar('Successfully added item', 'Close');
      this.closeModal();
    });
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  componentIsInvalid(control: string): boolean {
    return (this.addItemForm.get(control)!.touched || this.addItemForm.get(control)!.dirty) && !this.addItemForm.get(control)!.valid;
  }

  public validationMessages = {
    required: 'Please complete required field.',
  };

  openModal(templateRef, row:any) {
   this.rowToUpdate = row;
   console.log(this.rowToUpdate);
    if(row != undefined){
      //view clicked
      this.addUpdateItem = false;
      this.addItemForm.patchValue({
        the_Category: row.categoryName,
        the_ItemName: row.itemName,
        the_Price: row.itemCost
      });
    }else{
      //add items clicked
      this.addUpdateItem = true;
    }
   
    let dialogRef = this.dialog.open(templateRef, {
      width: '70%',
      height: 'auto'
      // data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  closeModal() {
    this.dialog.closeAll();
    this.getAllItems();
  }

  updateItem(){

    const formData = this.addItemForm.getRawValue();
    this.rowToUpdate.categoryName = formData.the_Category;
    this.rowToUpdate.itemName = formData.the_ItemName;
    this.rowToUpdate.itemCost = formData.the_Price;

    this.adminService.updateOrderItem(this.rowToUpdate).subscribe(result => {
      this.openSnackBar('Successfully updated item', 'Close');
      this.closeModal();
    });
  }

  deleteItem(){
    console.log("test",this.rowToUpdate);
    this.adminService.deleteOrderItem(this.rowToUpdate.id).subscribe(result => {
      this.openSnackBar('Successfully deleted item', 'Close');
      this.closeModal();
    });
  }
}
