import { createAction, props } from '@ngrx/store';
import { ProductDto } from '../models/product.dto';

export const loadProducts = createAction('[Product] Load Products');
export const loadProductsSuccess = createAction('[Product] Load Products Success', props<{ products: ProductDto[] }>());
export const loadProductsFailure = createAction('[Product] Load Products Failure', props<{ error: any }>());