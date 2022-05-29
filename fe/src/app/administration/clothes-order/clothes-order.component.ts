import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { InsertClothesOrderModel } from '../models/add-clothes-order-form-model';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { MatDialog } from '@angular/material';
interface itemValue {
  value: string;
}
export interface PeriodicElement {
  category: string;
  item: string;
  price: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99},
  {category: 'Formals', item: 'Blazer', price: 569.99},
  {category: 'Casual', item: 'Tshirt', price: 150},
  {category: 'Other', item: 'Keepnet', price: 299.99}
];

@Component({
  selector: 'app-clothes-order',
  templateUrl: './clothes-order.component.html',
  styleUrls: ['./clothes-order.component.scss']
})
export class ClothesOrderComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['category', 'item', 'team', 'gender','age', 'size', 'price','quantity','total'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  test:string = 'testering this is a test message to see if the full message doe in fact dispaly or not, but this will have to be seen.';
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  myModel: InsertClothesOrderModel = {} as any;
  addOrderItems:boolean = false;
  viewOrderItems:boolean = false;
  viewActiveOrders:boolean = false;

  category: itemValue[] = [
    {value: 'Battle dress'},
    {value: 'Formals'},
    {value: 'Other'},
  ];

  team: itemValue[] = [
    {value: 'PROTEA'},
    {value: 'SASACC'}
  ];

  gender: itemValue[] = [
    {value: 'Male'},
    {value: 'Female'},
    {value: 'N/A'}
  ];

  age: itemValue[] = [
    {value: 'Adult'},
    {value: 'Junior'},
    {value: 'N/A'}
  ];

  constructor(private formBuilder: FormBuilder,public dialog: MatDialog) { }
  addItemForm!: FormGroup;
  clothesOrderForm!: FormGroup;

  ngOnInit(): void {
    this.createForm();
  }

  navigateBack(){
    this.viewActiveOrders = false;
    this.viewOrderItems = false;
    this.addOrderItems = false;
  }
  toggleviewActiveOrders(){
    this.viewActiveOrders = true;
  }
  toggleViewItems(){
    this.viewOrderItems = true;
  }
  toggleAddItems(){
    this.addOrderItems = true;
  }

  createForm(): void {
    this.addItemForm = this.formBuilder.group({
      the_Category: ['', Validators.required],
      the_ItemName: ['', Validators.required],
      the_Price: ['', Validators.required]
    });

    this.clothesOrderForm = this.formBuilder.group({
      the_Category: ['', Validators.required],
      the_ItemName: ['', Validators.required],
      the_Price: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.addItemForm.invalid) {
      this.validateAllFormFields(this.addItemForm);
      return;
    }

    const formData = this.addItemForm.getRawValue();
    console.log("order form data test",formData);

    let today: object = new Date();

    //this.myModel.Province = formData.the_Province;
    //this.myModel.photos = this.photosObject;

    /*console.log(this.myModel);
    this.http.post('https://localhost:44351/InsertListing', this.myModel).pipe().subscribe(a => {

    }, (error) => {

    });
    console.log(formData);*/
  }
  onSubmitClothesOrder() {
    if (this.clothesOrderForm.invalid) {
      this.validateAllFormFields(this.clothesOrderForm);
      return;
    }

    const formData = this.clothesOrderForm.getRawValue();
    console.log("order form data test",formData);

    let today: object = new Date();

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
  componentOrderClothesIsInvalid(control: string): boolean {
    return (this.clothesOrderForm.get(control)!.touched || this.clothesOrderForm.get(control)!.dirty) && !this.clothesOrderForm.get(control)!.valid;
  }

  public validationMessages = {
    required: 'Please complete required field.',
  };

  proceedToSummary(){
    console.log("this works");
  }

  openModal(templateRef) {
    console.log("inside",templateRef);
    let dialogRef = this.dialog.open(templateRef, {
         width: 'auto',
         height: 'auto'
         // data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        // this.animal = result;
    });
}
}
