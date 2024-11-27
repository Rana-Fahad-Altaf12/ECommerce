import { createSelector } from '@ngrx/store';
import { ProductState } from './product.reducer';

export const selectProductState = (state: any) => state.product;

export const selectAllProducts = createSelector(
  selectProductState,
  (state: ProductState) => state.products
);

export const selectProductError = createSelector(
  selectProductState,
  (state: ProductState) => state.error
);