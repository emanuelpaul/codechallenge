import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ICompany } from './company';
import { CompanyService } from './company.service';

@Component({
  templateUrl: './company-edit.component.html'
})
export class CompanyEdit implements OnInit {
  company: ICompany = { name: '', exchange: '', isin: '', ticker: '', website: '', companyId: 0 };
  pageTitle: string = '';

  constructor(private route: ActivatedRoute, private router: Router, private companyService: CompanyService) {
  }

  ngOnInit(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
      this.pageTitle = 'Edit company data'
      this.companyService.getCompanyById(id).subscribe(company => this.company = company, _ => this.router.navigate(['/company-list']));
    } else {
      this.pageTitle = 'Add new company';
    }
  }

  submit() {
    if (this.company.companyId > 0) {
      this.companyService.updateCompany(this.company.companyId, this.company)
        .subscribe(_ => this.router.navigate(['/company-list']));
    } else {
      this.companyService.addCompany(this.company)
        .subscribe(_ => this.router.navigate(['/company-list']));
    }
  }
}
