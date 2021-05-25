import { Injectable } from '@angular/core';
import { Employee } from './employee.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  readonly baseURL = "https://localhost:44322/api/Employee"
  formData: Employee = new Employee();
  list: Employee[];

  postEmployee(){
    return this.http.post(this.baseURL, this.formData);
  }

  putEmployee() {
    return this.http.put(`${this.baseURL}/${this.formData.name}`, this.formData);
  }

  deleteEmployee(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList(){
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as Employee[]);
  }
}
