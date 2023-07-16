import { ProductService } from './../../Services/Product/product.service';
import { Component, Inject, OnInit } from '@angular/core';
import { BrandService } from '../../Services/Brand/brand.service';
import { CategoryService } from '../../Services/Category/category.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';

import { ConfirmationComponent } from '../confirmation/confirmation.component';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public Data: any,
    private BrandService: BrandService,
    private Fb: FormBuilder,
    private CategoryService: CategoryService,
    private ProductService: ProductService,
    public dialog: MatDialog,
    private matdaialog: MatDialogRef<CreateComponent>,
    private Toaster: ToastrService
  ) {}
  ngOnInit(): void {
    this.LoadBrands();
    this.LoadCategories();
    this.Form();
    console.log(this.Data);
  }
  newProductForm!: FormGroup;
  FormValues: any;
  Form() {
    this.newProductForm = this.Fb.group({
      Name: [this.Data?.name || '', [Validators.required]],
      Price: [this.Data?.price || '', [Validators.required]],
      Quantity: [this.Data?.quantity || '', [Validators.required]],
      File: ['', [Validators.required]],
      CategoryId: [this.Data?.categoryId || '', [Validators.required]],
      BrandId: [this.Data?.brandId || '', [Validators.required]],
    });
    this.FormValues = this.newProductForm.value;
  }
  AllBrands!: any;
  AllCategories!: any;
  Close() {
    let hasChanges = false;
    Object.keys(this.FormValues).forEach((item) => {
      if (this.FormValues[item] !== this.newProductForm.value[item]) {
        hasChanges = true;
      }
    });
    if (hasChanges) {
      const dialogRef = this.dialog.open(ConfirmationComponent, {
        disableClose: true,
        width: '750px',
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
        }
      });
    } else {
      this.matdaialog.close();
    }
  }
  LoadBrands() {
    this.BrandService.GetAll().subscribe((res) => {
      this.AllBrands = res;
    });
  }
  LoadCategories() {
    this.CategoryService.GetAll().subscribe((res) => {
      this.AllCategories = res;
    });
  }

  Create() {
    let MyData = new FormData();
    MyData.append('Name', this.newProductForm.value['Name']);
    MyData.append('Price', this.newProductForm.value['Price']);
    MyData.append('Quantity', this.newProductForm.value['Quantity']);
    MyData.append('CategoryId', this.newProductForm.value['CategoryId']);
    MyData.append('BrandId', this.newProductForm.value['BrandId']);
    MyData.append(
      'File',
      this.newProductForm.value['File'],
      this.newProductForm.value['File'].name
    );

    this.ProductService.Create(MyData).subscribe((res) => {
      this.Toaster.success('Success', 'Product Created Succesfuly');
      this.matdaialog.close(true);
    });
    console.log(this.newProductForm.value);
  }
  Update() {
    let MyData = new FormData();
    MyData.append('Name', this.newProductForm.value['Name']);
    MyData.append('Price', this.newProductForm.value['Price']);
    MyData.append('Quantity', this.newProductForm.value['Quantity']);
    MyData.append('CategoryId', this.newProductForm.value['CategoryId']);
    MyData.append('BrandId', this.newProductForm.value['BrandId']);
    MyData.append(
      'File',
      this.newProductForm.value['File'],
      this.newProductForm.value['File'].name
    );

    this.ProductService.Update(this.Data.id, MyData).subscribe((res) => {
      this.Toaster.success('Success', 'Product Updated Succesfuly');
      this.matdaialog.close(true);
    });
    console.log(this.newProductForm.value);
  }
  SelectImage($event: any) {
    this.newProductForm.get('File')?.setValue($event.target.files[0]);
  }
}
