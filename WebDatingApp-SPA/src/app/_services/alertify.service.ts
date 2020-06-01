import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

  confirm(mensagem: string, okCallBack: () => any) {
    alertify.confirm(mensagem, (e: any) => {
      if (e) {
        okCallBack();
      } else {}
    });
  }

  success(mesagem: string) {
    alertify.success(mesagem);
  }

  error(mesagem: string) {
    alertify.error(mesagem);
  }

  warning(mesagem: string) {
    alertify.warning(mesagem);
  }

  message(mesagem: string) {
    alertify.message(mesagem);
  }



}
