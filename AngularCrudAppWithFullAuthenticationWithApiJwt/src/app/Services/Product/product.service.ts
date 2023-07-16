import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private HttpClient: HttpClient) {}
  Create(Body: any) {
    return this.HttpClient.post(
      environment.BaseApi + 'api/Product/Create',
      Body
    );
  }

  GetAll() {
    return this.HttpClient.get(environment.BaseApi + 'api/Product/GetAll');
  }
  Delete(id: any) {
    return this.HttpClient.delete(
      environment.BaseApi + 'api/product/delete/' + id
    );
  }
  GetById(id: any) {
    return this.HttpClient.get(
      environment.BaseApi + 'api/product/GetById/' + id
    );
  }
  Update(id: any, body: any) {
    return this.HttpClient.put(
      environment.BaseApi + 'api/product/Update/' + id,
      body
    );
  }
}
