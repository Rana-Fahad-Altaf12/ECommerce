import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { ProductService } from '../service/product.service';
import { loadProducts, loadProductsSuccess, loadProductsFailure } from './product.actions';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class ProductEffects {
    constructor(
        private actions$: Actions,
        private productService: ProductService
    ) {}

    loadProducts$ = createEffect(() =>
        this.actions$.pipe(
            ofType(loadProducts),
            mergeMap(() =>
                this.productService.getProducts().pipe(
                    map(products => loadProductsSuccess({ products })),
                    catchError(error => 
                        of(loadProductsFailure({ error }))
                    )
                )
            )
        )
    );
}