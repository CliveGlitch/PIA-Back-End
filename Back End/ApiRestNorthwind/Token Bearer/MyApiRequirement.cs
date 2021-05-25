// se descargó este paquete para implementar una interfaz en esta clase, dandole todas las propiedades que esta tiene
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestNorthwind.Token_Bearer
{
    // con esta interfaz se podrá checar si el usuario pasa los requerimientos necesarios que pongamos
    // en este caso el Token Bearer JWT
    public class MyApiRequirement : IAuthorizationRequirement
    {
    }
}
