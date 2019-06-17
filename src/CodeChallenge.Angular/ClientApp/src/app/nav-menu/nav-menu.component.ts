import { Component } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { ILoginInfo } from '../authentication/LoginInfo';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  loginInfo: ILoginInfo;

  constructor(public authService: AuthenticationService) {
    this.authService.loginInformation.subscribe(x => this.loginInfo = x);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
  }
}
