import { Injectable } from '@angular/core';
// to use library that is imported from angular.json you have to type like this
declare let alertify: any; // we wont import because we already did it in angular.json, do this so it wont get any errors
@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

// confirm box asking if you want to confirm or not
// recieveing parameters message string and a function type any called okCallback
confirm(message: string, okCallback: () => any) {
  // confirm box asking if you want to confirm or not
  alertify.confirm(message, function(e) {
    // if the user clicks ok then call this function okcallback
    if (e) {
      okCallback();
    } else {}
  });
}

success(message: string) {
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}

warning(message: string) {
  alertify.warning(message);
}


message(message: string) {
  alertify.message(message);
}



}
