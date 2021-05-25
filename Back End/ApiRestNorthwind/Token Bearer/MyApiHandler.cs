using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiRestNorthwind.Token_Bearer
{
    // Esta clase se usa para poder llenar los requerimientos para aceptar
    // uso de los controladores.
    public class MyApiHandler : AuthorizationHandler<MyApiRequirement>
    {
        protected override Task HandleRequirementAsync(
           AuthorizationHandlerContext context, MyApiRequirement requirement)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (requirement == null)
                throw new ArgumentNullException(nameof(requirement));

            // dependiendo del Id del cliente se utiliza un tipo de chequeo
            var client_id = context.User.Claims
                 .FirstOrDefault(t => t.Type == "client_id");

            if (AccessTokenValid(client_id))
            {
                context.Succeed(requirement);
            }

            // si los requeimientos son exitosos se regresa que se logró
            return Task.CompletedTask;
        }

        // en este metodo se regresa si es valido el Token de acceso
        private bool AccessTokenValid(Claim client_id)
        {
            if (client_id != null && client_id.Value == "CC_STS_A")
            {
                return true;
            }

            if (client_id != null && client_id.Value == "CC_STS_B")
            {
                return true;
            }

            return false;
        }

    }
}
