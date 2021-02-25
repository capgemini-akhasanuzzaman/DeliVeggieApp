namespace ProductService.Extensions
{
    using DeliVeggieSharedLibrary.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using ProductService.Extensions.Translators;
    using System;
    using System.Threading.Tasks;

    internal sealed class ExceptionHanldingMiddleware
    {
        private readonly IOptions<RabbitMqConfig> _rabbitMqConfig;

        private readonly RequestDelegate _next;
        public ExceptionHanldingMiddleware(RequestDelegate next, IOptions<RabbitMqConfig> rabbitMqConfig)

        {
            _next = next;
            _rabbitMqConfig = rabbitMqConfig;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                //await ExceptionToHttpTranslator.Translate(httpContext, e);
                //var err = $"{_rabbitMqConfig?.Value?.ConnectionString} : is  the connection string";
                throw new Exception(e.ToString());
            }
        }
    }
}
