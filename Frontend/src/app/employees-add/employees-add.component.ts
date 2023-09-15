import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Employee } from '../models/Employee';
import { EmployeesService } from '../employees.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employees-add',
  templateUrl: './employees-add.component.html',
  styleUrls: ['./employees-add.component.scss']
})
export class EmployeesAddComponent {
  name: string = '';
  lastName: string = '';

  constructor(private employeesService: EmployeesService, private router: Router) { }

  onSubmit() {
    //TODO: Add better validation
    if (this.name == '' || this.lastName == '') {
      alert('Please fill all the fields.');
      return;
    }
    let employee: Employee = {
      Id: 0,
      Name: this.name,
      LastName: this.lastName
    }

    this.employeesService.addEmployee(employee).subscribe({
      next: (response: any) => {
        alert('Employee added successfully.');
        this.router.navigate(['/employeesList']);
      },
      error: (error: any) => {
        console.log('Error: ', error);
        alert('Something went wrong. Please try again later.');
      }
    });

  }
}
