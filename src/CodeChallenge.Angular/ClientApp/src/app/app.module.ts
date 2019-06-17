import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CompanyList } from './companies/company-list.component';
import { CompanyEdit } from './companies/company-edit-component';
import { CompanyDelete } from './companies/company-delete-component';
import { HttpErrorInterceptor } from './http-interceptors/error-interceptor';
import { AlertComponent } from './alert/alert.component';
import { LoginComponent } from './authentication/login.component';
import { JwtInterceptor } from './http-interceptors/jwt-interceptor';
import { AuthGuard } from './authentication/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CompanyList,
    CompanyEdit,
    CompanyDelete,
    AlertComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'company-list', component: CompanyList, canActivate: [AuthGuard] },
      { path: 'add-new-company', component: CompanyEdit, canActivate: [AuthGuard] },
      { path: 'company-edit/:id', component: CompanyEdit, canActivate: [AuthGuard] },
      { path: 'company-delete/:id', component: CompanyDelete, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
