import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule, FormsModule,
    RouterModule.forChild([
      { path: '', component: HomeComponent }
    ])
  ]
})
export class Ll1Module { }
