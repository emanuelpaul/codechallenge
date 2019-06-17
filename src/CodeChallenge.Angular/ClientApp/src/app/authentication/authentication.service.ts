import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILoginInfo } from './LoginInfo';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentLoginInformationSubject: BehaviorSubject<ILoginInfo>;
  public loginInformation: Observable<ILoginInfo>;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.currentLoginInformationSubject = new BehaviorSubject<ILoginInfo>(JSON.parse(localStorage.getItem('loginInformation')));
    this.loginInformation = this.currentLoginInformationSubject.asObservable();
  }

  login(username: string, password: string): Observable<ILoginInfo> {
    return this.httpClient.post<ILoginInfo>(`${this.baseUrl}/account/login`, { username: username, password: password })
      .pipe(map(loginSuccess => {
        if (loginSuccess && loginSuccess.token) {
          this.currentLoginInformationSubject.next(loginSuccess);
          localStorage.setItem('loginInformation', JSON.stringify(loginSuccess));
        }
        return loginSuccess;
      }));
  }

  public get loginInformationValue(): ILoginInfo {
    return this.currentLoginInformationSubject.value;
  }

  logout() {
    localStorage.removeItem("loginInformation");
    this.currentLoginInformationSubject.next(null);
  }
}
