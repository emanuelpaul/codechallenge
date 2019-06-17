import { Component, OnInit } from '@angular/core';
import { CompanyService } from './company.service';
import { ICompany } from './company';
@Component({
  templateUrl: './company-list.component.html'
})
export class CompanyList implements OnInit {

  companies: ICompany[] = [];

  constructor(private companyService: CompanyService) {
  }

  ngOnInit(): void {
    this.companyService.getCompanies().subscribe(
      companies => this.companies = companies);
  }
}
