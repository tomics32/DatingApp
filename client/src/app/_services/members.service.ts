import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Photo } from '../_models/photo';
import { PaginatedResponse } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { setPaginationHeaders } from './paginationHelper';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  baseUrl = environment.apiUrl;
  paginatedResult = signal<PaginatedResponse<Member> | null>(null);
  private userParams!: UserParams;
  
  constructor() {
    const user = JSON.parse(localStorage.getItem('user')!);
    this.userParams = new UserParams(user);
  }

  getUserParams(): UserParams {
    if (!this.userParams) {
      const saved = localStorage.getItem('userParams');
      if (saved) {
        this.userParams = Object.assign(new UserParams(this.accountService.currentUser()), JSON.parse(saved));
      } else {
        this.userParams = new UserParams(this.accountService.currentUser());
      }
    }
    return this.userParams;
  }

  resetUserParams(): UserParams{
    localStorage.removeItem('userParams');
    this.userParams = new UserParams(this.accountService.currentUser());
    console.log(this.userParams);
    return this.userParams;
  }
  
  setUserParams(params: UserParams) {
    this.userParams = params;
    localStorage.setItem('userParams', JSON.stringify(params));
  }
  
  getMembers() {
    const userParams = this.getUserParams();
  
    let params = setPaginationHeaders(userParams.pageNumber, userParams.pageSize);
  
    
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
  
    return this.http.get<PaginatedResponse<Member>>(this.baseUrl + 'users', { params })
      .subscribe({
        next: response => this.paginatedResult.set(response),
        error: err => console.log(err)
      });
  }
  

  

  getMember(username: string) {
    // const member = this.members().find(x => x.username === username);
    // if (member !== undefined) {
    //   return of(member);
    // }

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      // tap(() => {
      //   this.members.update(members => members.map(m => m.username === member.username ? member : m))
      // })
    )
  }

  setMainPhoto(photo: Photo) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photo.id, {}).pipe(
      // tap(() => {
      //   this.members.update(members => members.map(m => {
      //     if (m.photos.includes(photo)) {
      //       m.photoUrl = photo.url
      //     }
      //     return m;
      //   }))
      // })
    )
  }

  deletePhoto(photo: Photo) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photo.id).pipe(
      //   tap(() => {
      //     this.members.update(members => members.map(m => {
      //       if (m.photos.includes(photo)){
      //         m.photos = m.photos.filter(x => x.id !== photo.id);
      //       }
      //       return m
      //     }))
      //   })
    )
  }

}
