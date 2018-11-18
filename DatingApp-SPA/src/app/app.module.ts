import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';

import { NavComponent } from './nav/nav.component';
// import for input forms like the login
import { FormsModule } from '@angular/forms';
//
import { AuthService } from './_servicese/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      AuthService // servicefolder
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
