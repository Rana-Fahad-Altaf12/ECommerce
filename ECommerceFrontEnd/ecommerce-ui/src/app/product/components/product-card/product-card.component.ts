import { Component, Input } from '@angular/core';
import { ProductDto } from '../../models/product.dto';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {
  @Input() product!: ProductDto;
}