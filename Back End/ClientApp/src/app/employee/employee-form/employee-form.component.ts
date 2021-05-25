import { NgForOfContext } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/share/employee.service';
import { NgForm } from '@angular/forms';
import { Employee } from 'src/app/share/employee.model';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styles: [
  ]
})
export class EmployeeFormComponent implements OnInit {

  constructor(public service: EmployeeService, private toastr:ToastrService) { }
  
  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    this.service.postEmployee().subscribe(
      res =>{
        this.resetForm(form);
        this.toastr.success("Enviado con exito")
      },
      err =>{ console.log(err); }
    );
  }

  insertRecord(form: NgForm) {
    this.service.postEmployee().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Enviado exitosamente', 'Empleado registado')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putEmployee().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Cambio exitoso', 'Empleadoregistrado')
      },
      err => { console.log(err); }
    );
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData = new Employee();
  }
}
