import { AboutComponent } from './about/about.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PublicLayoutComponent } from './public-layout.component';

const routes: Routes = [
  {
    path: '', component: PublicLayoutComponent, children: [
      {path: '', component: HomeComponent},
      {path:'about',component:AboutComponent}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
