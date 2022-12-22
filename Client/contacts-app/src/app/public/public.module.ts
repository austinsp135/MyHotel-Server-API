import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { PublicLayoutComponent } from './public-layout.component';
import { AboutComponent } from './about/about.component';

@NgModule({
  declarations: [
    NavbarComponent,
    HomeComponent,
    PublicLayoutComponent,
    AboutComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
  ]
})
export class PublicModule { }
