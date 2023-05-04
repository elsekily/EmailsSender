import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { message } from '../models/message';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private messageCreatedSubject = new Subject<message>();
  
  
  constructor(private http: HttpClient) { }
  
  create(message:message) {
    return this.http.post<message>('/api/Message', message);
  }
  getMessages() {
    return this.http.get<message[]>('/api/Message');
  }
  getMessage(id:number) {
    return this.http.get<message>('/api/Message/' + id);
  }



  sendMessageCreatedEvent(message: message): void {
    this.messageCreatedSubject.next(message);
  }
  getMessageCreatedSubject(): Subject<message> {
    return this.messageCreatedSubject;
  }
}