import{O as V,P as F,t as E,u as N}from"./chunk-6JUBYSQI.js";import{$a as y,Ba as x,Ea as w,Fa as u,H as a,Kb as j,Mb as _,N as C,O as s,Pa as v,T as p,U as c,_a as k,da as d,ea as I,mb as B,pa as f,pb as T,ra as l,tb as M,va as o,wa as m,xa as S,ya as g,za as b}from"./chunk-V45PRMTZ.js";function Q(t,e){t&1&&(o(0,"div")(1,"p"),v(2,"There are no items in your basket"),m()())}function $(t,e){if(t&1){let h=x();g(0),o(1,"div",2)(2,"div",3)(3,"app-basket-summary",4),w("addItem",function(n){p(h);let r=u();return c(r.incrementQuantity(n))})("removeItem",function(n){p(h);let r=u();return c(r.removeItem(n))}),m()(),o(4,"div",3)(5,"div",5),S(6,"app-order-totals"),o(7,"div",6)(8,"a",7),v(9," Proceed to checkout "),m()()()()(),b()}}var L=(()=>{let e=class e{constructor(i){this.basketService=i}incrementQuantity(i){this.basketService.addItemToBasket(i)}removeItem(i){this.basketService.removeItemFromBasket(i.id,i.quantity)}};e.\u0275fac=function(n){return new(n||e)(I(E))},e.\u0275cmp=C({type:e,selectors:[["app-basket"]],decls:5,vars:6,consts:[[1,"container","mt-5"],[4,"ngIf"],[1,"container"],[1,"row"],[3,"addItem","removeItem"],[1,"col-6","offset-6"],[1,"d-grid"],["routerLink","/checkout",1,"btn","btn-outline-primary","py-2"]],template:function(n,r){n&1&&(o(0,"div",0),f(1,Q,3,0,"div",1),k(2,"async"),f(3,$,10,0,"ng-container",1),k(4,"async"),m()),n&2&&(d(),l("ngIf",y(2,2,r.basketService.basketSource$)===null),d(2),l("ngIf",y(4,4,r.basketService.basketSource$)))},dependencies:[B,j,N,V,T]});let t=e;return t})();var q=[{path:"",component:L}],P=(()=>{let e=class e{};e.\u0275fac=function(n){return new(n||e)},e.\u0275mod=s({type:e}),e.\u0275inj=a({imports:[_.forChild(q),_]});let t=e;return t})();var te=(()=>{let e=class e{};e.\u0275fac=function(n){return new(n||e)},e.\u0275mod=s({type:e}),e.\u0275inj=a({imports:[M,P,F]});let t=e;return t})();export{te as BasketModule};