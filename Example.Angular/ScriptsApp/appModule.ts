///<reference path="../node_modules/reflect-metadata/reflect-metadata.d.ts"/>"
import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, JsonpModule } from '@angular/http';

import { AppComponent }  from './mainApp';


@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule
    ],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }