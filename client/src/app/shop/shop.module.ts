import {NgModule} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ShopComponent} from './shop.component';
import {ProductItemComponent} from './product-item/product-item.component';
import {SharedModule} from "../shared/shared.module";


@NgModule({
    declarations: [
        ShopComponent,
        ProductItemComponent
    ],
    imports: [
        CommonModule,
        NgOptimizedImage,
        SharedModule
    ],
    exports: [
        ShopComponent
    ]
})
export class ShopModule {
}
