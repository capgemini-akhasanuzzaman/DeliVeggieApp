import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../models/Product';
import { ProductService } from '../services/product-service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: Product;

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      const id = +params.id;
      if (id && id > 0) {
        this.fetchProductDetails(id);
      }
    });

  }
  fetchProductDetails(productId) {
    this.productService.getProduct(productId)
      .subscribe((product: any) => {
        this.product = product.product;
      });
  }
}
