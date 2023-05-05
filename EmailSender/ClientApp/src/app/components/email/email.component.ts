import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { EmailService } from '../../services/email.service';
import { MessageRecipients } from 'src/app/models/MessageRecipients';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.css']
})
export class EmailComponent {
  emailControl = new FormControl('', [Validators.required, Validators.email]);
  emails: string[] = [];
  messageId: number = 0;
  message: message = { id: 0, subject: '', body: '' };
  messageRecipients:MessageRecipients = {messageId:0,RecipientEmailAddresses:[]}

  constructor(private route: ActivatedRoute,private messageService: MessageService,private emailService:EmailService) { }
  
  ngOnInit() {
    this.route.params.subscribe(p => {
      this.messageId = p['id'] | 0;
    });

    this.messageService.getMessage(this.messageId).subscribe(m => {
      this.message = m;
    });
   }
  
  addEmail() {
    const email = this.emailControl.value;
    
    if (!email) {
      return;
    }
    const trimmedEmail = email.trim();
    
    if (!trimmedEmail) {
      return;
    }
    if (!this.isValidEmail(trimmedEmail)) {
      return;      
    }
    if (!this.emails.includes(trimmedEmail)) {
      this.emails.push(trimmedEmail);
    }
    this.emailControl.reset();
  }
  
  isValidEmail(email: string): boolean {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
  }

  removeEmail(email: string) {
    this.emails = this.emails.filter((e) => e !== email);
  }

  submit() {
    this.messageRecipients.messageId = this.messageId;
    this.messageRecipients.RecipientEmailAddresses = this.emails;

    this.emailService.create(this.messageRecipients).subscribe(
      (response) => {
        alert('Request was successful!');

    },
      (error) => {
        alert('Request failed!');
      }
    );

    this.emails = [];
  }
}