import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../service/product.service';
import { ProductDto } from '../../models/product.dto';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product!: ProductDto;
  loading: boolean = true;

  constructor(private route: ActivatedRoute, private productService: ProductService) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.loadProduct(productId);
    }
  }

  loadProduct(id: string): void {
    this.productService.getProductById(id).subscribe(
      (data: ProductDto) => {
        this.product = data;
        this.loading = false;
      },
      (error) => {
        console.error('Error loading product', error);
        this.loading = false;
      }
    );
  }
}