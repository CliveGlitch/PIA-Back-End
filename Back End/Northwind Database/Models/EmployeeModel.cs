using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Northwind_Database.Models
{
    // Modelo de un empleado con los siguientes datos
    public class EmployeeModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }

    }
}
