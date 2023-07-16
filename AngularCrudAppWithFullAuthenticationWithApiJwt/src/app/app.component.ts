import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'app';
  lang: any;
  constructor(private translate: TranslateService) {
    if ('languge' in localStorage) {
      this.lang = localStorage.getItem('languge');
      translate.use(this.lang);
    } else {
      translate.use(this.translate.defaultLang);
    }
  }
}
