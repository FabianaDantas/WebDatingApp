import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MembersDetailComponent } from './members/members-detail/members-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesListComponent } from './likes-list/likes-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailResolver } from './_resolvers/members-detail.resolver';
import { MemberListResolver } from './_resolvers/members-list.resolver';
import { MembersEditComponent } from './members/members-edit/members-edit.component';
import { MemberEditResolver } from './_resolvers/members-edit.resolver';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MembersListComponent, resolve: {users: MemberListResolver}},
            { path: 'members/:id', component: MembersDetailComponent, resolve: {user: MemberDetailResolver}},
            { path: 'member/edit', component: MembersEditComponent, 
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChangesGuard]},
            { path: 'messages', component: MessagesComponent},
            { path: 'favs', component: LikesListComponent}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
