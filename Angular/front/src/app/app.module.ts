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


const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: ServiceApiComponent },
  { path: 'planets-count', component: PlanetsCountComponent },
  { path: 'heaviest-planet', component: HeaviestPlanetComponent },
  { path: 'discoverers', component: DiscoverersComponent },
  { path: 'hire-discoverer', component: HireDiscovererComponent },
  { path: 'stars-random', component: StarsRandomComponent },
  { path: 'stars-count', component: StarsCountComponent }

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
