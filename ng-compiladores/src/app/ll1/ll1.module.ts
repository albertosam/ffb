import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule, FormsModule, NgbModule,
    RouterModule.forChild([
      { path: '', component: HomeComponent }
    ])
  ]
})
export class Ll1Module { }
