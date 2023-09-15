import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { APIResponse } from './models/APIResponse';
import { Observable } from 'rxjs';
import { env } from 'env/env';
import { Employee } from './models/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<APIResponse> {
    let options = { params: {} };
    return this.http.get<APIResponse>(`${env.base_url}Employees`, options);
  }
  addEmployee(employee: Employee): Observable<APIResponse> {
    return this.http.post<APIResponse>(`${env.base_url}Employees`, employee);
  }

  getEmployee(id: number): Observable<APIResponse> {
    return this.http.get<APIResponse>(`${env.base_url}Employees/${id}`);
  }

  updateEmployee(id: number, employee: Employee): Observable<APIResponse> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    //Example of patch body to update only the name and last name, it can be used to update only one field
    //I prefer to make this example to show how to update only one field
    const body = JSON.stringify([
      { "op": "replace", "path": "/Name", "value": employee.Name },
      { "op": "replace", "path": "/LastName", "value": employee.LastName }
    ]);
    return this.http.patch<APIResponse>(`${env.base_url}Employees/${id}`, body, { 'headers': headers });
  }

  deleteEmployee(id: number): Observable<APIResponse> {
    return this.http.delete<APIResponse>(`${env.base_url}Employees/${id}`);
  }

}
