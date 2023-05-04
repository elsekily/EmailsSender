import { Component } from '@angular/core';
import { MessageService } from '../../services/message.service';
import { message } from '../../models/message';

@Component({
  selector: 'app-message-form',
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.css']
})
export class MessageFormComponent {
  constructor(private MessageService: MessageService) {
  }
  message: message = { id: 0, subject: '', body: '' };

  


  onSubmit() {
    this.MessageService.create(this.message).subscribe(m => {
      this.message = { id: 0, subject: '', body: '' };
      this.MessageService.sendMessageCreatedEvent(m);
    });
    
  }
}


