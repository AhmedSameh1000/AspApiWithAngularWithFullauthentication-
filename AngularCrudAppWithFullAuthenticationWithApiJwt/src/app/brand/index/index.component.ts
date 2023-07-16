import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateComponent } from '../create/create.component';
import { BrandService } from 'src/app/Services/Brand/brand.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css'],
})
export class IndexComponent implements OnInit {
  constructor(public dialog: MatDialog, private BrandService: BrandService) {}
  ngOnInit(): void {
    this.GetAll();
  }
  Brands: any;
  Show() {
    const dialogRef = this.dialog.open(CreateComponent, {
      width: '750px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.GetAll();
      }
    });
  }

  GetAll() {
    return this.BrandService.GetAll().subscribe((res) => {
      this.Brands = res;
    });
  }
}
