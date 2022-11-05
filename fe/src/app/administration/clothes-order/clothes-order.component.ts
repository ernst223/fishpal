import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { InsertClothesOrderModel } from '../models/add-clothes-order-form-model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material';
import { MatSort } from '@angular/material/sort';
interface itemValue {
  value: string;
}
export interface UserData {
  id: string;
  price: string;
  username:string;
  orderdate:string;
  item:string;
  status:string;
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
  selector: 'app-clothes-order',
  templateUrl: './clothes-order.component.html',
  styleUrls: ['./clothes-order.component.scss']
})
export class ClothesOrderComponent  {
  /*viewItems: boolean = false;
  viewOrders: boolean = false;

  viewitemsButton: boolean = true;
  viewOrdersButton: boolean = true;

  createOrderForm!: FormGroup;
  category: itemValue[] = [
    { value: 'Battle dress' },
    { value: 'Formals' },
    { value: 'Other' },
  ];
  team: itemValue[] = [
    { value: 'PROTEA' },
    { value: 'SASACC' },
    { value: 'N/A' },
  ];
  item: itemValue[] = [
    { value: 'Blazer' },
    { value: 'T-Shirt' },
    { value: 'Keepnet' },
  ];

  displayedColumns: string[] = ['id', 'username', 'orderdate', 'item', 'price', 'status', 'actions'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private formBuilder: FormBuilder, public dialog: MatDialog) {
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
    console.log(name);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  createNewUser(id: number): UserData {
    return {
      id: id.toString(),
      price: "399.99",
      username: "Basson",
      orderdate: "2022/05/23",
      item: "Blazer",
      status: "Paid",
    };
  }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
    this.createOrderForm = this.formBuilder.group({
      the_Category: ['', Validators.required],
      the_Item: ['', Validators.required],
      the_Team: ['', Validators.required],
      the_Qty: ['', Validators.required],
      the_Size: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.createOrderForm.invalid) {
      this.validateAllFormFields(this.createOrderForm);
      return;
    }

    const formData = this.createOrderForm.getRawValue();
    console.log("order form data test", formData);

    let today: object = new Date();

    //this.myModel.Province = formData.the_Province;
    //this.myModel.photos = this.photosObject;

   
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
    return (this.createOrderForm.get(control)!.touched || this.createOrderForm.get(control)!.dirty) && !this.createOrderForm.get(control)!.valid;
  }

  public validationMessages = {
    required: 'Please complete required field.',
  };

  openModal(templateRef) {
    console.log("inside", templateRef);
    let dialogRef = this.dialog.open(templateRef, {
      width: '40%',
      height: 'auto'
      // data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }

  closeModal(templateRef) {
    this.dialog.closeAll();
  }

  toggleViewItems(){
    this.viewItems = true;
    this.viewitemsButton = false;
    this.viewOrdersButton = false;
  }

  toggleViewOrders(){
    this.viewItems = false;
    this.viewOrders = true;
    this.viewitemsButton = false;
    this.viewOrdersButton = false;
  }

  navigateBack(){
    this.viewItems = false;
    this.viewOrders = false;
    this.viewitemsButton = true;
    this.viewOrdersButton = true;
  }
*/
}

