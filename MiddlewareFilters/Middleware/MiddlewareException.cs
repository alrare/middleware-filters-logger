using System.Net;

namespace MiddlewareFilters.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;

        public MiddlewareException(RequestDelegate next)
        {
            _next = next;
        }
        public class Error
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new Error
                {
                    StatusCode = context.Response.StatusCode,
                    Message = e.Message
                };

                await context.Response.WriteAsync(text: error.ToString());
            }
        }
    }
}
