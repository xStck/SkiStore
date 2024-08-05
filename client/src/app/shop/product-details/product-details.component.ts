import {Component, OnInit} from '@angular/core';
import {Product} from "../../shared/models/product";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";
import {BreadcrumbService} from "xng-breadcrumb";
import {BasketService} from "../../basket/basket.service";
import {take} from "rxjs";

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
    product?: Product;
    quantity = 1;
    quantityBasket = 0;

    constructor(private shopService: ShopService,
                private activatedRoute: ActivatedRoute,
                private breadcrumbService: BreadcrumbService,
                private basketService: BasketService) {
        this.breadcrumbService.set('@productDetails', ' ');
    }

    get buttonText() {
        return this.quantityBasket === 0 ? 'Add to basket' : 'Update basket';
    }

    ngOnInit(): void {
        this.loadProduct();
    }

    loadProduct() {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        if (id) this.shopService.getProduct(+id).subscribe({
            next: product => {
                this.product = product;
                this.breadcrumbService.set('@productDetails', product.name);
                this.basketService.basketSource$.pipe(take(1)).subscribe({
                    next: basket => {
                        const item = basket?.items.find(x => x.id === +id);
                        if (item) {
                            this.quantity = item.quantity;
                            this.quantityBasket = item.quantity;
                        }
                    }
                })
            },
            error: error => console.log(error)
        });
    }

    incrementQuantity() {
        this.quantity++;
    }

    decrementQuantity() {
        if (this.quantity > 0)
            this.quantity--;
    }

    updateBasket() {
        if (this.product) {
            if (this.quantity > this.quantityBasket) {
                const itemsToAdd = this.quantity - this.quantityBasket;
                this.quantityBasket += itemsToAdd;
                this.basketService.addItemToBasket(this.product, itemsToAdd);
            } else {
                const itemsToRemove = this.quantityBasket - this.quantity;
                this.quantityBasket -= itemsToRemove;
                this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
            }
        }
    }
}
