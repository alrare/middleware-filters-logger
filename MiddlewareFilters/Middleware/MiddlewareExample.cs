using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using ProtoBuf.Reflection;
using System.Diagnostics;
using System.Net;

namespace MiddlewareFilters.Middleware
{
    public class MiddlewareExample
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MiddlewareExample(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(typeof(MiddlewareExample));
        }

        public async Task Invoke(HttpContext context)
        {
            Guid traceId = Guid.NewGuid();
            _logger.LogTrace($"Request {traceId} iniciado");
            _logger.LogInformation($"Middleware: Inicia Request {traceId} iniciado");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            await _next(context);

            //Después del request
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            _logger.LogTrace($"La Request {traceId} ha llevado {elapsedTime}");
            _logger.LogInformation($"Middleware: Después del Request {traceId} ha llevado {elapsedTime}");
        }

    }
}