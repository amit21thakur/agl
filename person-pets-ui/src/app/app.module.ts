import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PetsComponent } from './components/pets/pets.component';
import { PetsService } from './services/pets.service';

@NgModule({
  declarations: [
    AppComponent,
    PetsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [PetsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
