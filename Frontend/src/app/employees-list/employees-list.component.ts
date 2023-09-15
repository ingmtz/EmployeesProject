import { Component, OnInit } from '@angular/core';
import { EmployeesService } from '../employees.service';
import { Employee } from '../models/Employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.scss']
})
export class EmployeesListComponent implements OnInit {
  employees: Employee[] = [];
  constructor(private employeesService: EmployeesService, private router: Router) { }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees() {
    this.employeesService.getEmployees().subscribe({
      next: (response: any) => {
        this.employees = response.Result as Employee[];
      },
      error: (error: any) => {
        console.log('Error: ', error);
        alert('Something went wrong. Please try again later.');
      },
      complete: () => { }
    });
  }

  addEmployee() {
    this.router.navigate(['/employeesAdd']);
  }

  editEmployee(employee: Employee) {
    this.router.navigate(['/employeesEdit', employee.Id]);
  }

  deleteEmployee(employee: any) {
    //It is not recommended to delete an employee, instead you can add a field to the employee table called "IsDeleted" and set it to true
    if (!confirm('Are you sure you want to delete this employee?')) {
      return;
    }
    this.employeesService.deleteEmployee(employee.Id).subscribe({
      next: (response: any) => {
        alert('Employee deleted successfully.');
        this.getEmployees();
      },
      error: (error: any) => {
        console.log('Error: ', error);
        alert('Something went wrong. Please try again later.');
      },
      complete: () => { }
    });

  }
}
