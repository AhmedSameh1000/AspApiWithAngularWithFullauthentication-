import { Component } from '@angular/core';
import { AuthService } from '../Services/Auth/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  lang: any = 'en';
  constructor(public auth: AuthService, private translate: TranslateService) {
    this.lang = translate.currentLang;
  }
  selected = 'option0';
  ChangeLang() {
    if (this.lang == 'en') {
      localStorage.setItem('languge', 'ar');
    } else {
      localStorage.setItem('languge', 'en');
    }
    window.location.reload();
  }
}
