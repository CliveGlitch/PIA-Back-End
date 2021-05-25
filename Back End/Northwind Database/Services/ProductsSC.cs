using Northwind_Database.DataAccess;
using Northwind_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Database.Services
{
    public class ProductsSC : BaseSC
    {
        // GET ALL
        // Al llamar esta función se solicitan todos los productos de la base de datos
        public IQueryable<Product> GetAllProducts()
        {
            return dataContext.Products.Select(s => s);
        }

        // GET BY ID
        // Con esta función se solicita un producto en base a un ID mandado por el usuario
        // Si se encuentra en la base de datos, se regresa el producto, si no se notifica que no se encontró 
        public Product GetProductById(int id)
        {
            var product = GetAllProducts().Where(w => w.ProductId == id).FirstOrDefault();

            if (product == null)
                throw new Exception("El id solicitado para el empleado que quieres obtener, no existe");

            return product;
        }

        // DELETE
        // Usando la función get by id buscamos con un id el producto que se quiere eliminar
        public void DeleteProductById(int id)
        {
            var product = GetProductById(id);
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
        }

        // PUT
        // Usando la función get by id buscamos con un id el producto que se quiere modificar
        public void UpdateProductNameById(int id, string newName)
        {
            Product currentProduct = GetProductById(id);

            if (currentProduct == null)
                throw new Exception("No se encontró el empleado con el ID proporcionado");

            currentProduct.ProductName = newName;
            dataContext.SaveChanges();
        }

        // POST
        // Se agrega un nuevo producto a la base de datos llenando los datos pedidos.
        public void AddProduct(ProductModel newProduct)
        {
            // notación parecida a JSON
            var newProductRegister = new Product()
            {
                ProductName = newProduct.Name
            };

            dataContext.Products.Add(newProductRegister);
            dataContext.SaveChanges();
        }
    }
}
