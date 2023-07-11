import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ServiceApiComponent } from './service-api/service-api.component';
import { HttpClientModule } from '@angular/common/http';
import { PlanetsCountComponent } from './planets-count/planets-count.component';
import { RouterModule, Routes } from '@angular/router';
import { HeaviestPlanetComponent } from './heaviest-planet/heaviest-planet.component';
import { DiscoverersComponent } from './discoverers/discoverers.component';
import { HireDiscovererComponent } from './hire-discoverer/hire-discoverer.component';
import { StarsRandomComponent } from './stars-random/stars-random.component';
import { StarsCountComponent } from './stars-count/stars-count.component';
import { ShipNewComponent } from './ship-new/ship-new.component';
import { StarsystemAllComponent } from './starsystem-all/starsystem-all.component';
import { GalaxyAllComponent } from './galaxy-all/galaxy-all.component';
import { ShipAllComponent } from './ship-all/ship-all.component';
import { FireDiscovererComponent } from './fire-discoverer/fire-discoverer.component';


const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: ServiceApiComponent },
  { path: 'planets-count', component: PlanetsCountComponent },
  { path: 'heaviest-planet', component: HeaviestPlanetComponent },
  { path: 'discoverers', component: DiscoverersComponent },
  { path: 'hire-discoverer', component: HireDiscovererComponent },
  { path: 'fire-discoverer', component: FireDiscovererComponent },
  { path: 'stars-random', component: StarsRandomComponent },
  { path: 'stars-count', component: StarsCountComponent },
  { path: 'ship-new', component: ShipNewComponent },
  { path: 'galaxy-all', component: GalaxyAllComponent },
  { path: 'starsystem-all', component: StarsystemAllComponent },
  { path: 'ship-all', component: ShipAllComponent }

];

@NgModule({
  declarations: [
    AppComponent,
    ServiceApiComponent,
    PlanetsCountComponent,
    HeaviestPlanetComponent,
    DiscoverersComponent,
    HireDiscovererComponent,
    StarsRandomComponent,
    StarsCountComponent,
    ShipNewComponent,
    StarsystemAllComponent,
    GalaxyAllComponent,
    ShipAllComponent,
    FireDiscovererComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule { }
