import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-sorting',
  template: `
    <select [(ngModel)]="sortBy" (change)="onSortChange()" class="form-select">
      <option value="popularity">Sort by Popularity</option>
      <option value="price">Sort by Price</option>
      <option value="rating">Sort by Rating</option>
    </select>
  `
})
export class SortingComponent {
  @Input() sortBy: string = 'popularity';
  @Output() sortChange = new EventEmitter<string>();

  onSortChange(): void {
    this.sortChange.emit(this.sortBy);
  }
}