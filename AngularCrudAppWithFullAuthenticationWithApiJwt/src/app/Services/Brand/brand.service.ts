import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  constructor(private Http: HttpClient) {}

  Create(Body: any) {
    return this.Http.post(environment.BaseApi + 'api/Brand/Create', Body);
  }
  GetAll() {
    return this.Http.get(environment.BaseApi + 'api/Brand/GetAll');
  }
}
