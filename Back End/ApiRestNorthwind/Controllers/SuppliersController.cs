using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind_Database.DataAccess;
using Northwind_Database.Models;
using Northwind_Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// En este y los demas controladores se usan sus servicios respectivos para poder crear el CRUD

namespace ApiRestNorthwind.Controllers
{
    // Se activa la poliza CORS que se ha creado en Startup.cs llamada MyPolicy
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private SuppliersSC supplierService = new SuppliersSC();

        // GET: api/<SuppliersController>
        // Este metodo se usa para obtener todos los suppliers de la base de datos
        [HttpGet]
        public List<Supplier> Get()
        {
            var supplier = new SuppliersSC().GetAllSuppliers().ToList();
            return supplier;
        }

        // GET api/<SuppliersController>/5
        // En este metodo se entrega todos los datos del supplier que se quiere buscar
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = supplierService.GetSupplierById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<SuppliersController>
        // En este metodo se agrega un nuevo supplier a la base de datos con los datos entregados
        [HttpPost]
        public IActionResult Post([FromBody] SupplierModel newSupplier)
        {
            try
            {
                supplierService.AddSupplier(newSupplier);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<SuppliersController>/5
        // En este metodo se modifica los datos de un supplier en especifico
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string company)
        {
            try
            {
                supplierService.UpdateSupplierCompanyNameById(id, company);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<SuppliersController>/5
        // En este metodo se elimina un supplier en especifico de la base de datos
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                supplierService.DeleteSupplierById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
