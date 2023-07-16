import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private Http: HttpClient) {}

  Create(Body: any) {
    return this.Http.post(environment.BaseApi + 'api/Category/Create', Body);
  }
  GetAll() {
    return this.Http.get(environment.BaseApi + 'api/Category/GetAll');
  }
}
