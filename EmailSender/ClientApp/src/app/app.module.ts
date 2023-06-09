import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MessageFormComponent } from './components/message-form/message-form.component';
import { MessageService } from './services/message.service';
import { CommonModule } from '@angular/common';
import { MessageListComponent } from './components/message-list/message-list.component';
import { HomeComponent } from './components/home/home.component';
import { EmailComponent } from './components/email/email.component';
import { EmailService } from './services/email.service';

@NgModule({
  declarations: [
    AppComponent,
    MessageFormComponent,
    MessageListComponent,
    HomeComponent,
    EmailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
       { path: 'sendmail/:id', component: EmailComponent },

    ])
  ],
  providers: [
    MessageService,
    EmailService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
