import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_servicese/auth.service';
import { AlertifyService } from '../_servicese/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // input sending value from parent to child component
  // parent => home , child => register component

  @Output() cancelRegister = new EventEmitter();



  model: any = {};
  constructor(private autService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.autService.register(this.model).subscribe(() => {
    this.alertify.success('registration successful');
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    // inside emit() can be any data such as object etc. but here we are sending boolean
    // send data to parent component using emit
    this.cancelRegister.emit(false);

  }



}
