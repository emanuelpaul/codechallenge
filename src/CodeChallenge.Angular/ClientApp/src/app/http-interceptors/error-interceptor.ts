import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertService } from "../alert/alert.service";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private alertService: AlertService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(err => {
      let error = err.error.message || err.statusText;
      this.alertService.clear();
      if (err.status !== 401) {
        if (err.status == 400) {
          if (err.error.errors) {
            error = '';
            for (let fieldName in err.error.errors) {
              for (let errorText of err.error.errors[fieldName]) {
                this.alertService.error(errorText);
              }
            }
          }
        }

        if (error) {
          this.alertService.error(error, true);
        }
      }
      return throwError(error);
    }))
  }
}
