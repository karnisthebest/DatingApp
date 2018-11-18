import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// normally a component will automatically be injected
// but a service will not, so we need to inject it
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
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
            }
          })
        )
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }
}
