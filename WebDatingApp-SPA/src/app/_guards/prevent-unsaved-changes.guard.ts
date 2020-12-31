import { Injectable } from '@angular/core';
import { CanDeactivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { MembersEditComponent } from '../members/members-edit/members-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<MembersEditComponent> {
  canDeactivate(component: MembersEditComponent) {
    if (component.editForm.dirty) {
      return confirm('Tem certeza que deseja continuar? Suas alterações serão perdidas');
    }

    return true;
  }

}
