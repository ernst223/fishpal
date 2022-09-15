import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {
  contactUsForm: FormGroup;
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
  }
  
  createForm(): void {
    this.contactUsForm = this.formBuilder.group({
      the_NameSurname: ['', Validators.required],
      the_Email: ['', Validators.required],
      the_Message: ['', Validators.required],
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
    return (this.contactUsForm.get(control)!.touched || this.contactUsForm.get(control)!.dirty) && !this.contactUsForm.get(control)!.valid;
  }

  public validationMessages = {
    required: 'Please complete required field.',
  };
}
