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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CompanyList,
    CompanyEdit,
    CompanyDelete,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'company-list', component: CompanyList },
      { path: 'add-new-company', component: CompanyEdit },
      { path: 'company-edit/:id', component: CompanyEdit },
      { path: 'company-delete/:id', component: CompanyDelete },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
