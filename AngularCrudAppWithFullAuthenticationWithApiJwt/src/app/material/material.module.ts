import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';
const materials = [
  BrowserAnimationsModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatButtonToggleModule,
  MatButtonModule,
  MatIconModule,
  MatListModule,
  MatCardModule,
  MatTabsModule,
  MatGridListModule,
  MatSidenavModule,
  MatToolbarModule,
  MatTableModule,
  MatDialogModule,
  TranslateModule.forChild({
    extend: true,
  }),
];

@NgModule({
  declarations: [],
  imports: [materials],
  exports: [materials],
})
export class MaterialModule {}
