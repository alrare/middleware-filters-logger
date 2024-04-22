using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareFilters.Filters
{

    //// Utilizando ActionFilterAttribute
    //public class FiltersExample : ActionFilterAttribute
    //{
    //    private readonly ILogger<FiltersExample> _logger;

    //    public FiltersExample(ILogger<FiltersExample> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        //_logger.LogTrace("Log antes del método");
    //        _logger.LogInformation("Filters OnActionExecuting: Log antes del método");
    //        base.OnActionExecuting(context);
    //    }

    //    public override void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        //_logger.LogTrace("Log despues del método");
    //        _logger.LogInformation("Filters OnActionExecuted: Log después del método");
    //        base.OnActionExecuted(context);
    //    }

    //}


    //// Utilizando IAsyncActionFilter
    public class FiltersExample : IAsyncActionFilter
    {
        private readonly ILogger<FiltersExample> _loggerAudit;
        private readonly ILogger<FiltersExample> _loggerOperational;
        private readonly ILogger<FiltersExample> _loggerApplication;


        public FiltersExample(ILogger<FiltersExample> loggerAudit, ILogger<FiltersExample> loggerOperational, ILogger<FiltersExample> loggerApplication)
        {
            _loggerAudit = loggerAudit;
            _loggerOperational = loggerOperational;
            _loggerApplication = loggerApplication;     
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //context

            //Acceso a toda la acción que se activo 
            //context.ActionDescriptor

            //Acceso a httpContext
            //context.HttpContext

            //Acceso a todos los datos de ruta
            //context.RouteData

            try
            {
                _loggerAudit.LogInformation("Filtro LogInformation: Auditoría antes del método");
                _loggerApplication.LogWarning("Filtro LogWarning: Aplicación LogTrace antes del método");

            } 
            catch (Exception)
            {
                _loggerOperational.LogError("Filtro LogError: Operacional LogError antes del método");
            }

       
            await next();

            try
            {
                _loggerAudit.LogInformation("Filtro LogInformation: Auditoría después del método");
                _loggerApplication.LogWarning("Filtro LogWarning: Aplicación después del método");
            }
            catch (Exception)
            {
                _loggerOperational.LogError("Filtro LogError: Operacional después método");
            }    
        }
    }
}
