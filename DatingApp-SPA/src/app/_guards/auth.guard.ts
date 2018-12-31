import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_servicese/auth.service';
import { AlertifyService } from '../_servicese/alertify.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService) {}

  canActivate(): boolean {
    // if user is logged in
    // return true
    if (this.authService.loggedIn()) {
      return true;
    }
    // if user is not logged in
    // alert popup
    this.alertify.error('You shall not pass!!!');
    // go back to home page
    this.router.navigate(['/home']);
    return false;
  }
}
