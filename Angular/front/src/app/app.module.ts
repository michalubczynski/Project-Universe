import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GalaxyComponent } from './galaxy/galaxy.component';
import { GalaxiesComponent } from './galaxies/galaxies.component';
import { GalaxyFormComponent } from './galaxy-form/galaxy-form.component';

@NgModule({
  declarations: [
    AppComponent,
    GalaxyComponent,
    GalaxiesComponent,
    GalaxyFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
