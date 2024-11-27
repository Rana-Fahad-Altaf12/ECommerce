import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { ProductDto } from '../../models/product.dto';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: ProductDto[] = [];
  filteredProducts: ProductDto[] = [];
  paginatedProducts: ProductDto[] = [];
  loading: boolean = true;
  currentPage: number = 1;
  itemsPerPage: number = 6;
  
  selectedCategory: string = '';
  selectedSubcategory: string = '';
  searchTerm: string = '';
  sortBy: string = 'popularity';
  categories: { id: number, name: string }[] = [];
  subcategories: { id: number, name: string, categoryId: number }[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadProducts();
  }

  loadCategories(): void {
    this.productService.getCategories().subscribe(
      (data: { id: number, name: string }[]) => {
        this.categories = data;
      },
      (error) => {
        console.error('Error loading categories', error);
      }
    );
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(
      (data: ProductDto[]) => {
        this.products = data;
        this.filteredProducts = data;
        this.updatePagination();
        this.loading = false;
      },
      (error) => {
        console.error('Error loading products', error);
        this.loading = false;
      }
    );
  }

  onCategoryChange(): void {
    this.selectedSubcategory = '';
    this.subcategories = [];
    if(this.selectedCategory !== ''){
    this.productService.getSubcategories(this.selectedCategory).subscribe(
      (data: { id: number, name: string, categoryId: number }[]) => {
        this.subcategories = data;
        this.filterProducts();
      },
      (error) => {
        console.error('Error loading subcategories', error);
      }
    );
  }else{
    this.filterProducts();
  }
  }
  onSubCategoryChange(): void {
    this.filterProducts();
  }

  onSearch(): void {
    this.filterProducts();
  }

  onSortChange(sortBy: string): void {
    this.sortBy = sortBy;
    this.filterProducts();
  }

  filterProducts(): void {
    this.filteredProducts = this.products.filter(product => {
      const matchesCategory = this.selectedCategory ? product.categoryId.toString() === this.selectedCategory : true;
      const matchesSubcategory = this.selectedSubcategory ? product.subcategoryId.toString() === this.selectedSubcategory : true;
      const matchesSearchTerm = product.name.toLowerCase().includes(this.searchTerm.toLowerCase());
      return matchesCategory && matchesSubcategory && matchesSearchTerm;
    });
    this.updatePagination();
  }

  updatePagination(): void {
    this.paginatedProducts = this.filteredProducts.slice((this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage);
  }

  changePage(page: number): void {
    if (page < 1 || page > Math.ceil(this.filteredProducts.length / this.itemsPerPage)) return;
    this.currentPage = page;
    this.updatePagination();
  }
  
  getImageUrl(imageUrl: string): string {
    return imageUrl && this.isValidImageUrl(imageUrl) ? imageUrl : 'assets/no-image-available.png';
  }

  onImageError(event: Event): void {
    const target = event.target as HTMLImageElement;
    target.src = 'assets/no-image-available.png';
  }

  private isValidImageUrl(url: string): boolean {
    return url.startsWith('http') || url.startsWith('assets');
  }
}