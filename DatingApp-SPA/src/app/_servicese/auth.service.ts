import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
// normally a component will automatically be injected
// but a service will not, so we need to inject it
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;


  constructor(private http: HttpClient) {}

  login(model: any) {
    // .post(the action controller, the value in the body)
    return (
      this.http
        .post(this.baseUrl + 'login', model)
        // after sending login value to the api controller
        // it will return a response as JWT token
        // this will store this token locally
        .pipe(
          // reponse will be jwt token
          map((response: any) => {
            // store token in user const
            const user = response;
            // if there is something inside user
            if (user) {
              // store the jwt token locally
              localStorage.setItem('token', user.token);
              // store decoded token so we could get the info ex. username to show in the nav bar
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
              console.log(this.decodedToken);

            }
          })
        )
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // if it is not expired then return true vice versa
    return !this.jwtHelper.isTokenExpired(token);
  }
}
