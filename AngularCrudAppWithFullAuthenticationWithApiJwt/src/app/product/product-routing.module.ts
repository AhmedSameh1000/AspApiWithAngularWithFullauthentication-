import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { UpdateComponent } from './update/update.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  { path: 'ProdCreate', component: CreateComponent },
  { path: 'ProdUpdate', component: UpdateComponent },
  { path: 'ProdIndex', component: IndexComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductRoutingModule {}
