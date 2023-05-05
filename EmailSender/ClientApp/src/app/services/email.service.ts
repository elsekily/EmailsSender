import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageRecipients } from '../models/MessageRecipients';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  constructor(private http: HttpClient) { }


  create(messageRecipients:MessageRecipients) {
    return this.http.post<MessageRecipients>('/api/Email', messageRecipients);
  }
}
