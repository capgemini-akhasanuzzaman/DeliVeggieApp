import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from 'src/app/product/models/Product';
import { ProductList } from 'src/app/product/models/ProductList';
import { environment } from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productServiceUrl = 'http://localhost:9000/api/' + 'products'

  constructor(private http: HttpClient) { }

  getProducts(): Observable<ProductList> {
    return this.http.get<ProductList>(this.productServiceUrl);
  }
  getProduct(productId: any): Observable<Product> {
    var url = this.productServiceUrl + '/' + productId;
    return this.http.get<Product>(url);
  }
}
