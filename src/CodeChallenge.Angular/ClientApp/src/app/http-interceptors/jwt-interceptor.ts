import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { AuthenticationService } from "../authentication/authentication.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthenticationService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let loginInformation = this.authService.loginInformationValue;
    if (loginInformation && loginInformation.token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${loginInformation.token}`
        }
      });
    }
    return next.handle(req);
  }
}
