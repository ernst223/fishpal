import { AfterViewInit, Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { InsertClothesOrderModel } from '../models/add-clothes-order-form-model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { AdministrationService } from '../administration.service';

interface itemValue {
  value: string;
}
export interface UserData {
  id: string;
  name: string;
  price: string;
  size: string;
}

/** Constants used to fill up our data base. */
const FRUITS: string[] = [
  'S',
  'M',
  'L',
  'XL',
  '34/36',
  '48',
  'XXL',
  '34',
];
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
  addUpdateItem:boolean = true;
  clothesItemsModal: InsertClothesOrderModel = {} as any
  addItemForm!: FormGroup;
  category: itemValue[] = [
    { value: 'Battle dress' },
    { value: 'Formals' },
    { value: 'Other' },
  ];

  displayedColumns: string[] = ['id', 'name', 'price', 'actions'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private adminService: AdministrationService,private formBuilder: FormBuilder, public dialog: MatDialog) {
    const users = Array.from({ length: 100 }, (_, k) => this.createNewUser(k + 1));

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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

  /** Builds and returns a new User. */
  createNewUser(id: number): UserData {
    const name =
      NAMES[Math.round(Math.random() * (NAMES.length - 1))]

    return {
      id: id.toString(),
      name: name,
      price: Math.round(Math.random() * 100).toString(),
      size: FRUITS[Math.round(Math.random() * (FRUITS.length - 1))],
    };
  }

  ngOnInit(): void {
    this.createForm();
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
    
    this.clothesItemsModal.CategoryName = formData.the_ItemName;
    this.clothesItemsModal.ItemName = formData.the_ItemName;
    this.clothesItemsModal.Amount = formData.the_Price


    this.adminService.insertOrderItem(this.clothesItemsModal).subscribe(result => {
    });
    //this.myModel.Province = formData.the_Province;
    //this.myModel.photos = this.photosObject;

    /*console.log(this.myModel);
    this.http.post('https://localhost:44351/InsertListing', this.myModel).pipe().subscribe(a => {

    }, (error) => {

    });
    console.log(formData);*/
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

  openModal(templateRef, rowVal:any) {
    if(rowVal != undefined){
      this.addUpdateItem = false;
    }else{
      this.addUpdateItem = true;
    }
   
    let dialogRef = this.dialog.open(templateRef, {
      width: '70%',
      height: 'auto'
      // data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      // this.animal = result;
    });
  }

  closeModal(templateRef) {
    this.dialog.closeAll();
  }
}
