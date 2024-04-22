using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProtoBuf.Reflection;

namespace MiddlewareFilters.Filters
{
    public class FiltersExceptionHandler : IExceptionFilter
    {

        public class Error
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

        public void OnException(ExceptionContext context)
        {
            //Acceso a toda la acción que se activo
            //context.Exception
            //context.Result
            //context.ActionDescriptor
            //context.HttpContext
            //context.RouteData

            var error = new Error
            {
                StatusCode = 500,
                Message = context.Exception.Message

            };

            context.Result = new JsonResult(error) {StatusCode = 500};
        }
    }
}
