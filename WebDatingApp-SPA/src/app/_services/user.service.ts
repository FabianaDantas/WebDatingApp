import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
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


  getUsers(page?: number, itemsPerPage?: number, userParams?): Observable<PaginatedResult<User[]>> {
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber',page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (userParams != null) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);
    }

    return this.http.get<User[]>(this.baseUrl, { observe: 'response', params }).pipe(  
      map(response => {
        paginatedResult.result = response.body;
        if(response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
          return paginatedResult;
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
