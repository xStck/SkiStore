import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AccountService} from "../../account/account.service";
import {map} from "rxjs";

export const authGuard: CanActivateFn = (route, state) => {
    let accountService = inject(AccountService);
    let router = inject(Router);
    return accountService.currentUser$.pipe(
        map(auth => {
            if (auth) return true;
            else {
                router.navigate(['/account/login'], {queryParams: {returnUrl: state.url}});
                return false;
            }
        })
    );
};
