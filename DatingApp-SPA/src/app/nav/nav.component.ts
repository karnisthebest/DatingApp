import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_servicese/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  // store username and password that will then send to check in the server
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    // send login value to the login function in auth service
    // then it will return as an observable so we have to subscribe
    // next console log
    this.authService.login(this.model).subscribe(next => {
      console.log('logged in successfully');
    }, error => {
      console.log(error);
    });
  }

  // check if there's still token so we know we are still logged in
  loggedIn() {
    // get jwt token from the local storage and store in token var
    const token = localStorage.getItem('token');
    // !! =>
    // return true if there is a token
    // return false if there isn't a token
    return !!token;
  }

  // remove jwt token when logged out
  loggedOut() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
