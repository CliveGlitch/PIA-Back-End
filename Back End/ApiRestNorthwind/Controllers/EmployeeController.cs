using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind_Database.Services;
using Northwind_Database.DataAccess;
using Northwind_Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

// En este y los demas controladores se usan sus servicios respectivos para poder crear el CRUD

namespace ApiRestNorthwind.Controllers
{
    // Se activa la poliza CORS que se ha creado en Startup.cs llamada MyPolicy
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeSC employeeService = new EmployeeSC();

        // GET: api/<EmployeeController>
        [HttpGet]
        // Se usa la poliza de Token Bearer JWT en este verbo
        // No se podrá usar este verbo hasta que se entregué un Token valido
        [Authorize(AuthenticationSchemes = "SchemeStsA,SchemeStsB", Policy = "MyPolicy")]
        // Este metodo se usa para obtener todos los empleados de la base de datos
        public List<Employee> Get()
        {
            var employees = new EmployeeSC().GetAllEmployees().ToList();
            return employees;
        }

        // GET api/<EmployeeController>/5
        // En este metodo se entrega todos los datos del empleado que se quiere buscar
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = employeeService.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<EmployeeController>
        // En este metodo se agrega un nuevo empleado a la base de datos con los datos entregados
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel newEmployee)
        {
            try
            {
                employeeService.AddEmployee(newEmployee);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        // En este metodo se modifica los datos de un empleado en especifico
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string name)
        {
            try
            {
                employeeService.UpdateEmployeeFirstNameById(id, name);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<EmployeeController>/5
        // En este metodo se elimina un empleado en especifico de la base de datos
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                employeeService.DeleteEmployeeById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
