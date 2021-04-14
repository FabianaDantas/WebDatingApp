import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LOCALE_ID, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';
import { LoadingInterceptorProvider } from './_interceptors/loading.interceptor';

import { MemberDetailResolver } from './_resolvers/members-detail.resolver';
import { MemberListResolver } from './_resolvers/members-list.resolver';
import { MemberEditResolver } from './_resolvers/members-edit.resolver';

import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MembersCardComponent } from './members/members-card/members-card.component';
import { MembersDetailComponent } from './members/members-detail/members-detail.component';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { LikesListComponent } from './likes-list/likes-list.component';
import { MessagesComponent } from './messages/messages.component';
import { MembersEditComponent } from './members/members-edit/members-edit.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { appRoutes } from './routes';
import { NgxGalleryModule } from 'ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { registerLocaleData } from '@angular/common';
import localeBr from '@angular/common/locales/pt';
import { TimeagoIntl, TimeagoModule, TimeagoFormatter,TimeagoCustomFormatter } from 'ngx-timeago';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

registerLocaleData(localeBr, 'pt');

export function tokenGetter() {
   return localStorage.getItem('token');
 }

 export class CustomHammerConfig extends HammerGestureConfig  {
   overrides = {
       pinch: { enable: false },
       rotate: { enable: false }
   };
}


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MembersListComponent,
      LikesListComponent,
      MessagesComponent,
      MembersCardComponent,
      MembersDetailComponent,
      MembersEditComponent,
      PhotoEditorComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TabsModule.forRoot(),
      BrowserAnimationsModule,
      RouterModule.forRoot(appRoutes),
      FileUploadModule,
      NgxGalleryModule,
      NgxSpinnerModule,
      TimeagoModule.forRoot({
         intl: { provide: TimeagoIntl, useClass: TimeagoIntl },
         formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter },
      }),
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ["localhost:5000"],
           blacklistedRoutes: ["localhost:5000/api/auth"],
         },
       }),
      PaginationModule.forRoot(),
      ButtonsModule.forRoot()
   ],
   providers: [
      ErrorInterceptorProvider,
      LoadingInterceptorProvider,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditResolver,
      PreventUnsavedChangesGuard,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
      { provide: LOCALE_ID, useValue: 'pt' }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
