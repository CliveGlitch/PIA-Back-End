using Northwind_Database.DataAccess;
using Northwind_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Database.Services
{

    public class EmployeeSC : BaseSC
    {
        // GET BY ID
        // Con esta función se solicita un empleado en base a un ID mandado por el usuario
        // Si se encuentra en la base de datos, se regresa el empleado, si no se notifica que no se encontró 
        public Employee GetEmployeeById(int id)
        {
            var employee = GetAllEmployees().Where(w => w.EmployeeId == id).FirstOrDefault();

            if (employee == null)
                throw new Exception("El id solicitado para el empleado que quieres obtener, no existe");

            return employee;
        }

        // GET ALL
        // Al llamar esta función se solicitan todos los empleados de la base de datos
        public IQueryable<Employee> GetAllEmployees()
        {
            return dataContext.Employees.Select(s => s);
        }

        // DELETE
        // Usando la función get by id buscamos con un id el empleado que se quiere eliminar
        public void DeleteEmployeeById(int id)
        {
            var employee = GetEmployeeById(id);
            dataContext.Employees.Remove(employee);
            dataContext.SaveChanges();
        }

        // PUT
        // Usando la función get by id buscamos con un id el empleado que se quiere modificar
        public void UpdateEmployeeFirstNameById(int id, string newName)
        {
            Employee currentEmployee = GetEmployeeById(id);

            if (currentEmployee == null)
                throw new Exception("No se encontró el empleado con el ID proporcionado");

            currentEmployee.FirstName = newName;
            dataContext.SaveChanges();
        }

        // POST
        // Se agrega un nuevo empleado a la base de datos llenando los datos pedidos.
        public void AddEmployee(EmployeeModel newEmployee)
        {
            // notación parecida a JSON
            var newEmployeeRegister = new Employee()
            {
                FirstName = newEmployee.Name,
                LastName = newEmployee.Surname,


            };

            dataContext.Employees.Add(newEmployeeRegister);
            dataContext.SaveChanges();
        }
    }
}
