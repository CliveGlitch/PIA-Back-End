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
    public class ProductsController : ControllerBase
    {
        private ProductsSC productService = new ProductsSC();

        // GET: api/<ProductsController>
        // Este metodo se usa para obtener todos los productos de la base de datos
        [HttpGet]
        public List<Product> Get()
        {
            var product = new ProductsSC().GetAllProducts().ToList();
            return product;
        }

        // GET api/<ProductsController>/5
        // En este metodo se entrega todos los datos del producto que se quiere buscar
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<ProductsController>
        // En este metodo se agrega un nuevo producto a la base de datos con los datos entregados
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel newProduct)
        {
            try
            {
                productService.AddProduct(newProduct);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<ProductsController>/5
        // En este metodo se modifica los datos de un producto en especifico
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string Name)
        {
            try
            {
                productService.UpdateProductNameById(id, Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<ProductsController>/5
        // En este metodo se elimina un producto en especifico de la base de datos
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                productService.DeleteProductById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
