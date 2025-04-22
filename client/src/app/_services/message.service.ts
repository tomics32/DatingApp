import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { PaginatedResponse } from '../_models/pagination';
import { Message } from '../_models/message';
import { setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  paginatedResult = signal<PaginatedResponse<Message> | null>(null);

  getMessages(pageNumber: number, pageSize: number, container: string) {
    let params = setPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);

    this.http.get<Message[]>(this.baseUrl + 'messages', {observe: 'response', params}).subscribe({
      next: response => {
        const pagination = JSON.parse(response.headers.get('Pagination')!);
        this.paginatedResult.set({
          items: response.body || [],
          ...pagination
        });
      }
    });
  }
}
