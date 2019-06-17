import { Component, OnInit } from '@angular/core';
import { CompanyService } from './company.service';
import { ICompany } from './company';
import { AlertService } from '../alert/alert.service';
@Component({
  templateUrl: './company-list.component.html'
})
export class CompanyList implements OnInit {

  companies: ICompany[] = [];

  constructor(private companyService: CompanyService, private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.alertService.info('Loading...');
    this.companyService.getCompanies().subscribe(
      companies => {
        this.alertService.clear();
        this.companies = companies;
      });
  }
}
