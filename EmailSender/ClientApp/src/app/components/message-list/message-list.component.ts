import { Component } from '@angular/core';
import { message } from 'src/app/models/message';
import { MessageService } from '../../services/message.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent {

  messages: message[] = [];
  
  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.messageService.getMessages().subscribe(m => {
      this.messages = m;
    });

    this.messageService.getMessageCreatedSubject().subscribe(createdMessage => {
      this.messages.unshift(createdMessage);
    });
  }
}
