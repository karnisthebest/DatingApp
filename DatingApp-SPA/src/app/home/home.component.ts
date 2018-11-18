import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;


  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  registerToggle() {
    this.registerMode = true;
  }



  // receive boolearn false from child component register as false
  cancelRegisterMode(registerMode: boolean) {
    // set the real registerMode in here to be false
    this.registerMode = registerMode;
  }

}
