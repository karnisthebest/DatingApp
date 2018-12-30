import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_servicese/auth.service';
import { AlertifyService } from '../_servicese/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  // store username and password that will then send to check in the server
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    // send login value to the login function in auth service
    // then it will return as an observable so we have to subscribe
    // next console log
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('logged in successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

  // check if there's still token so we know we are still logged in
  loggedIn() {
    // this will return true if the token is not expired
    return this.authService.loggedIn();
  }

  // remove jwt token when logged out
  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

}
