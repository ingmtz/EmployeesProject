import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeesService } from '../employees.service';
import { Employee } from '../models/Employee';

@Component({
  selector: 'app-employees-edit',
  templateUrl: './employees-edit.component.html',
  styleUrls: ['./employees-edit.component.scss']
})
export class EmployeesEditComponent implements OnInit {
  receivedId: any = 0;
  name: string = '';
  lastName: string = '';

  constructor(private employeesService: EmployeesService, private router: Router, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log('params: ', params);
      
      this.receivedId = params['id'];
      this.getEmployee();
    });
  }

  getEmployee() {
    this.employeesService.getEmployee(this.receivedId).subscribe({
      next: (response: any) => {
        let employee: Employee = response.Result as Employee;
        this.name = employee.Name;
        this.lastName = employee.LastName;
      },
      error: (error: any) => {
        console.log('Error: ', error);
        alert('Something went wrong. Please try again later.');
      },
      complete: () => {}
    });
  }

  onSubmit() {
    //TODO: Add better validation
    if (this.name == '' || this.lastName == '') {
      alert('Please fill all the fields.');
      return;
    }
    let employee: Employee = {
      Id: this.receivedId,
      Name: this.name,
      LastName: this.lastName
    }

    this.employeesService.updateEmployee(this.receivedId, employee).subscribe({
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
