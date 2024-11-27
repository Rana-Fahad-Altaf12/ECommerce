import { createReducer, on } from '@ngrx/store';
import { loadProductsSuccess, loadProductsFailure } from './product.actions';
import { ProductDto } from '../models/product.dto';

export interface ProductState {
  products: ProductDto[];
  error: any;
}

export const initialState: ProductState = {
  products: [],
  error: null,
};

export const productReducer = createReducer(
  initialState,
  on(loadProductsSuccess , (state, { products }) => ({
    ...state,
    products: products,
    error: null,
  })),
  on(loadProductsFailure, (state, { error }) => ({
    ...state,
    error: error,
  }))
);