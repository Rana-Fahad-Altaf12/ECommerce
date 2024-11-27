import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductDto } from '../models/product.dto';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    private basePath = environment.apiUrl;
    private apiUrl = `${this.basePath}/product`; 

  constructor(private http: HttpClient) {}

  getProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.apiUrl}/list`);
  }

  getProductById(id: string): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/${id}`);
  }

  getCategories(): Observable<{ id: number, name: string }[]> {
    return this.http.get<{ id: number, name: string }[]>(`${this.apiUrl}/categories`);
  }
  getSubcategories(category: string): Observable<{ id: number, name: string, categoryId: number }[]> {
    return this.http.get<{ id: number, name: string, categoryId: number }[]>(`${this.apiUrl}/subcategories?categoryId=${category}`);
  }
}