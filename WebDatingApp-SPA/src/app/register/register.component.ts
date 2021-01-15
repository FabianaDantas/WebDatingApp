import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private authService: AuthService, private router: Router,
        private alertify: AlertifyService, private fb: FormBuilder) { }

  ngOnInit() {
    var maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() - 18);
    this.bsConfig = {
      containerClass: 'theme-default',
      maxDate: maxDate,
      dateInputFormat: 'DD/MM/YYYY'
    }
    
    this.createForm();
  }

  createForm() {
    this.registerForm = this.fb.group({
      gender: ['feminino'],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      confirmPassword: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      knownAs: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { 'mismatch' : true };
  }

  register() {
    if(this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('Registro efetuado com sucesso.');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/members']);
        });
      });
    }

    console.log(this.registerForm.value);
  }

  cancel() {
    // estamos emitindo um boolean false para a classe pai (home), com output
    this.cancelRegister.emit(false);
    this.alertify.warning('Registro cancelado');
  }

}
