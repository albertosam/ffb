import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: 'll1', loadChildren: () => import('./ll1/ll1.module').then(a => a.Ll1Module) }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
