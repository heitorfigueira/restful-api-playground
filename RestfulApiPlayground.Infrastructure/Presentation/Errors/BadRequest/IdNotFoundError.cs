using Microsoft.AspNetCore.Mvc;
using RestfulApiPlayground.Infrastructure.Presentation.Errors;
using System;

namespace ResftulApiPlayground.Exceptions
{
    public class IdNotFoundError : ErrorException
    {
        private const string _errorMessage = "Nenhum recurso foi encontrado com esse id.";
        private static Delegate _escapeMethod = () =>
        {
            return new BadRequestObjectResult(_errorMessage);
        };

        public IdNotFoundError() 
            : base(_errorMessage, _escapeMethod)
        {

        }

        public IdNotFoundError(Exception inner)
    :       base(_errorMessage, _escapeMethod, inner)
        {

        }
    }
}
