import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NgOptimizedImage} from "@angular/common";
import {HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withInterceptors} from "@angular/common/http";
import {CoreModule} from "./core/core.module";
import {HomeModule} from "./home/home.module";
import {ErrorInterceptor} from "./core/interceptors/error.interceptor";
import {LoadingInterceptor} from "./core/interceptors/loading.interceptor";
import {jwtInterceptor} from "./core/interceptors/jwt.interceptor";

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        NgOptimizedImage,
        HttpClientModule,
        CoreModule,
        HomeModule
    ],
    providers: [{
        provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true
    }, {
        provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true
    }, provideHttpClient(withInterceptors([jwtInterceptor]))],
    bootstrap: [AppComponent]
})
export class AppModule {
}
