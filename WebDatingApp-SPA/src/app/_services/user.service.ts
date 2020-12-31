import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';


/* Antes de pegar pelo auth0
const httpOptions = {
  headers : new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
}
*/

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'users/';
  users: User[] = [];

  constructor(private http: HttpClient) { }


  getUsers(): Observable<User[]> {
    if (this.users.length > 0) return of(this.users);
    return this.http.get<User[]>(this.baseUrl).pipe(
      map(users => {
        this.users = users;
        return users;
      })
    ) ;
  };

  getUser(id: any): Observable<User> {
    const user = this.users.find(x => x.id === id);
    if(user !== undefined) return of(user);
    return this.http.get<User>(this.baseUrl + id);
  };
  updateUser(id: any, user: User): Observable<User> {
    return this.http.put<User>(this.baseUrl + id, user).pipe(
      map(() => {
        const index = this.users.indexOf(user);
        this.users[index] = user;
        return user;
      })
    );
  };

  setMainPhoto(userId: number, id: number) {
    return this.http.post(this.baseUrl + userId + '/photos/' + id + '/setMain/', {});
  }

  deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + userId + '/photos/' + id);
  }


}
