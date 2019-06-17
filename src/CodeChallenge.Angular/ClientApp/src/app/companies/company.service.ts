import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICompany } from './company';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  getCompanies(): Observable<ICompany[]> {
    return this.httpClient.get<ICompany[]>(`${this.baseUrl}/companies`);
  }

  getCompanyById(id: number): Observable<ICompany> {
    return this.httpClient.get<ICompany>(`http://localhost:5000/api/companies/${id}`);
  }

  updateCompany(id: number, company: ICompany): Observable<Object> {
    return this.httpClient.put(`http://localhost:5000/api/companies/${id}`, company);
  }

  addCompany(company: ICompany): Observable<ICompany> {
    return this.httpClient.post<ICompany>('http://localhost:5000/api/companies', company);
  }

  deleteCompany(companyId: number): Observable<Object> {
    return this.httpClient.delete(`http://localhost:5000/api/companies/${companyId}`);
  }
}
