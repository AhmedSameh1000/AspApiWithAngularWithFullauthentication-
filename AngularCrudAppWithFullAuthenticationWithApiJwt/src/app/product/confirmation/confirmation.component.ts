import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css'],
})
export class ConfirmationComponent {
  constructor(
    public dialog: MatDialog,
    private matdaialog: MatDialogRef<ConfirmationComponent>
  ) {}
  Confirm() {
    this.dialog.closeAll();
  }
  Close() {
    this.matdaialog.close();
  }
}
