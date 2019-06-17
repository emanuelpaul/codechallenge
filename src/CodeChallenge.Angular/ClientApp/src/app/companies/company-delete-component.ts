import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ICompany } from './company';
import { CompanyService } from './company.service';

@Component({
  templateUrl: './company-delete.component.html'
})
export class CompanyDelete implements OnInit {
  company: ICompany;
  pageTitle: string = '';

  constructor(private route: ActivatedRoute, private router: Router, private companyService: CompanyService) {
  }

  ngOnInit(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    if (id > 0) {
      this.companyService.getCompanyById(id).subscribe(company => this.company = company, _ => this.router.navigate(['/company-list']));
    } else {
      this.router.navigate(['/company-list'])
    }
  }

  submit() {
    this.companyService.deleteCompany(this.company.companyId)
      .subscribe(_ => this.router.navigate(['/company-list']));
  }
}
