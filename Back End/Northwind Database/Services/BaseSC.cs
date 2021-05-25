using Northwind_Database.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Database.Services
{
    // Clase que se encarga de ser la herencia para todos los demas servicios donde tenemos el data context a usar.
    public class BaseSC
    {
        protected NORTHWNDContext dataContext = new NORTHWNDContext();
    }
}
