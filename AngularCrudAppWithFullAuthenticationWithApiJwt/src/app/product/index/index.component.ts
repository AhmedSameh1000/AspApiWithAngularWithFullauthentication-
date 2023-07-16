import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateComponent } from '../create/create.component';
import { ProductService } from 'src/app/Services/Product/product.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css'],
})
export class IndexComponent {
  constructor(
    public dialog: MatDialog,
    private ProductService: ProductService,
    private Toaster: ToastrService
  ) {}
  ngOnInit(): void {
    this.GetAll();
  }

  AllProduct: any;

  GetAll() {
    this.ProductService.GetAll().subscribe((res) => {
      this.AllProduct = res;
      console.log(res);
    });
  }
  Brands: any;
  Show() {
    const dialogRef = this.dialog.open(CreateComponent, {
      width: '1200px',
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.GetAll();
      }
    });
  }

  Update(data: any) {
    const dialogRef = this.dialog.open(CreateComponent, {
      width: '1200px',
      disableClose: true,
      data: data,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.GetAll();
      }
    });
  }
  Delete(id: any) {
    this.ProductService.Delete(id).subscribe((res) => {
      console.log(res);
      this.Toaster.success('Product Deleted Succefuly');

      this.GetAll();
    });
  }
}
