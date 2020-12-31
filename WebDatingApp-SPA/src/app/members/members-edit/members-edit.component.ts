import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-members-edit',
  templateUrl: './members-edit.component.html',
  styleUrls: ['./members-edit.component.css']
})
export class MembersEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  // Serve para quando a pessoa editar e fechar o navegador, aparecer um alerta
  @HostListener('window:beforeunload', ['$event']) 
  unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  };
  user: User;
  photoUrl: string;
  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private userService: UserService,
    private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });

    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);

  }

  updateUser() {
    console.log(this.user);
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.success('Perfil atualizado com sucesso!');
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.error('Erro ao atualizar perfil');
    });
  }

  updateMainPhoto(photoUrl){
    this.user.photoUrl = photoUrl;
  }

}
