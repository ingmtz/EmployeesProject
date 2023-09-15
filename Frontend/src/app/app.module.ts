import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeesListComponent } from './employees-list/employees-list.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { EmployeesAddComponent } from './employees-add/employees-add.component';
import { FormsModule } from '@angular/forms';
import { EmployeesEditComponent } from './employees-edit/employees-edit.component';

const appRoute: Routes = [
  { path: '', redirectTo: 'employeesList', pathMatch: 'full' },
  { path: 'employeesList', component: EmployeesListComponent },
  { path: 'employeesAdd', component: EmployeesAddComponent },
  { path: 'employeesEdit/:id', component: EmployeesEditComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    EmployeesListComponent,
    EmployeesAddComponent,
    EmployeesEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoute)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
