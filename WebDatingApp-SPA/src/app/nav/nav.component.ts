import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Login efetuado com sucesso!');
    }, error => {
      console.log('Erro Login');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // a exclamaçao dupla seta a variavel como boolean, retornando true se estiver preenchida e false caso contrario
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Log out');
  }
}
