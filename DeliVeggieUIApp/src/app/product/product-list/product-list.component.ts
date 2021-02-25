import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../models/Product';
import { ProductService } from '../services/product-service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: Product[];
  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.fetchProducts();
  }
  fetchProducts() {
    this.productService.getProducts()
      .subscribe((products) => {
        this.products = products.productList;
      });
  }

  editProduct(productId) {
    this.router.navigate(['/product', productId]);
  }
}
