using Microsoft.AspNetCore.Mvc;
using RestfulApiPlayground.Infrastructure.Presentation.Errors;
using System;

namespace ResftulApiPlayground.Exceptions
{
    public class InvalidIdArgumentError : ErrorException
    {
        private const string _errorMessage = "Argumento inválido, por favor forneça um id válido.";
        private static Delegate _escapeMethod = () =>
        {
            return new BadRequestObjectResult(_errorMessage);
        };

        public InvalidIdArgumentError()
            : base(_errorMessage, _escapeMethod)
        {

        }

        public InvalidIdArgumentError(Exception inner)
            : base(_errorMessage, _escapeMethod, inner)
        {

        }
    }
}
