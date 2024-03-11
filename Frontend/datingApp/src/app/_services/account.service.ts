import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../_models/user';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  users: any = [];
  baseUrl = environment.apiUrl;
  private CurrentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.CurrentUserSource.asObservable();
  constructor(private http:HttpClient) { }

  login(model:any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response:User)=>{
        const user = response;
        if(user){
          this.setCurrentUser(user)
        }
      })
    );
  }
  register(model:any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user =>{
        if(user)
        this.setCurrentUser(user);
      })
    )
  }
  setCurrentUser(user:User){
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.CurrentUserSource.next(user);
  }
  
  logout(){
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);
  }
  getUsers(){
    return this.http.get(this.baseUrl +'user');
  }

  getDecodedToken(token:string){
    return JSON.parse(atob(token.split('.')[1]));

  }
}
