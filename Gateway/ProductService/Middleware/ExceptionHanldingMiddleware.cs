namespace ProductService.Extensions
{
    using Microsoft.AspNetCore.Http;
    using ProductService.Extensions.Translators;
    using System;
    using System.Threading.Tasks;

    internal sealed class ExceptionHanldingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHanldingMiddleware(RequestDelegate next)
            => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await ExceptionToHttpTranslator.Translate(httpContext, e);
            }
        }
    }
}
