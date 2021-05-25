import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../share/employee.service';
import { Employee } from "../share/employee.model";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styles: [
  ]
})
export class EmployeeComponent implements OnInit {

  constructor(public service: EmployeeService,  private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: Employee) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.service.deleteEmployee(id)
        .subscribe(
          res => {
            this.service.refreshList();
            this.toastr.error("Eliminado exitosamente", 'Empleado registrado');
          },
          err => { console.log(err) }
        )
    }
  }
}
