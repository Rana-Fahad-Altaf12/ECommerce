import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  template: `
     <nav aria-label="Page navigation">
      <ul class="pagination">
        <li class="page-item" [class.disabled]="currentPage === 1">
          <a class="page-link" (click)="changePage(currentPage - 1)">Previous</a>
        </li>
        <li class="page-item" *ngFor="let page of totalPages" [class.active]="page === currentPage">
          <a class="page-link" (click)="changePage(page)">{{ page }}</a>
        </li>
        <li class="page-item" [class.disabled]="currentPage === totalPages.length">
          <a class="page-link" (click)="changePage(currentPage + 1)">Next</a>
        </li>
      </ul>
    </nav>
  `,
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent {
  @Input() currentPage: number = 1;
  @Input() totalItems: number = 0;
  @Input() itemsPerPage: number = 6;
  @Output() pageChange = new EventEmitter<number>();

  get totalPages(): number[] {
    return Array.from({ length: Math.ceil(this.totalItems / this.itemsPerPage) }, (_, i) => i + 1);
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages.length) return;
    this.pageChange.emit(page);
  }
}