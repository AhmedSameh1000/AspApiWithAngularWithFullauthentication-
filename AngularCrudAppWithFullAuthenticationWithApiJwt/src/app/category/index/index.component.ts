import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CategoryService } from '../../Services/Category/category.service';
import { CreateComponent } from '../create/create.component';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css'],
})
export class IndexComponent {
  constructor(
    public dialog: MatDialog,
    private CategoryService: CategoryService
  ) {}
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
    return this.CategoryService.GetAll().subscribe((res) => {
      this.Brands = res;
    });
  }
}
