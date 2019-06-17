import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "./authentication.service";
import { AlertService } from "../alert/alert.service";

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  returnUrl: string;
  username: string;
  password: string;

  constructor(private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    private alertService: AlertService) {

  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/company-list';
  }

  submit() {
    this.authService.login(this.username, this.password).subscribe(
      _ => this.router.navigate([this.returnUrl]),
      _ => {
        if (this.username && this.password) {
          this.alertService.error('Invalid user or password')
        }
      });
  }
}
