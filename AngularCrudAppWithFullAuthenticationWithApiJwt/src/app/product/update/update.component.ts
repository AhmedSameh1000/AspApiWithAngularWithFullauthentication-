import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProductService } from '../../Services/Product/product.service';
import { BrandService } from '../../Services/Brand/brand.service';
import { CategoryService } from '../../Services/Category/category.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css'],
})
export class UpdateComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public Data: any,
    private ProductService: ProductService,
    private Fb: FormBuilder,
    private BrandService: BrandService,
    private CategoryService: CategoryService
  ) {}
  ngOnInit(): void {
    this.productById();
    this.loadBrands();
    this.loadCategories();
    this.Form();
  }

  newProductForm!: FormGroup;
  Form() {
    this.newProductForm = this.Fb.group({
      Name: [this.product?.name, [Validators.required]],
      Price: [this.product?.price, [Validators.required]],
      Quantity: [this.product?.quantity, [Validators.required]],
      File: ['', [Validators.required]],
      CategoryId: [this.product?.categoryId, [Validators.required]],
      BrandId: [this.product?.BrandId, [Validators.required]],
    });
  }

  product: any;

  productById() {
    this.ProductService.GetById(this.Data).subscribe((res) => {
      this.product = res;
      console.log(res);
    });
  }
  Update() {
    this.ProductService.Update(this.Data, this.newProductForm.value).subscribe(
      (res) => {
        console.log(res);
      }
    );
  }
  AllBrands: any;
  AllCategories: any;
  loadBrands() {
    this.BrandService.GetAll().subscribe((res) => {
      this.AllBrands = res;
    });
  }

  loadCategories() {
    this.CategoryService.GetAll().subscribe((res) => {
      this.AllCategories = res;
    });
  }
}
