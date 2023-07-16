import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BrandService } from 'src/app/Services/Brand/brand.service';
import { CategoryService } from 'src/app/Services/Category/category.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent {
  constructor(
    public dialog: MatDialog,
    public MatDialog: MatDialogRef<CreateComponent>,
    private Fb: FormBuilder,
    private CategoryService: CategoryService,
    private Toaster: ToastrService
  ) {}
  ngOnInit(): void {
    this.FormData();
  }

  Close() {
    this.dialog.closeAll();
  }
  newBrandForm!: FormGroup;
  FormData() {
    this.newBrandForm = this.Fb.group({
      Name: ['', Validators.required],
    });
  }
  Create() {
    this.CategoryService.Create(this.newBrandForm.value).subscribe((res) => {
      console.log(res);
      this.Toaster.success('success', 'Created Succesfuly');
      this.MatDialog.close(true);
    });
  }
}
