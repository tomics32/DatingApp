import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { PaginatedResponse } from '../_models/pagination';
import { setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class LikesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  likeIds = signal<number[]>([]);
  paginatedResult = signal<PaginatedResponse<Member> | null>(null);

  toggleLike(targetId: number){
    return this.http.post(this.baseUrl + "likes/" + targetId, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number){
    let params = setPaginationHeaders(pageNumber, pageSize);

    params = params.append('predicate', predicate);

    return this.http.get<PaginatedResponse<Member>>(this.baseUrl + "likes", {params}).subscribe({
        next: response => this.paginatedResult.set(response)
      });
  }

  getLikesIds() {
    return this.http.get<number[]>(this.baseUrl + "likes/list").subscribe({
      next: ids => this.likeIds.set(ids)
    })
  }

}
