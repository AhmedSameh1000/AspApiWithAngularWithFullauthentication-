import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { BrandService } from '../../Services/Brand/brand.service';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    public MatDialog: MatDialogRef<CreateComponent>,
    private Fb: FormBuilder,
    private BrandService: BrandService,
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
    this.BrandService.Create(this.newBrandForm.value).subscribe((res) => {
      console.log(res);
      this.Toaster.success('success', 'Created Succesfuly');
      this.MatDialog.close(true);
    });
  }
}
