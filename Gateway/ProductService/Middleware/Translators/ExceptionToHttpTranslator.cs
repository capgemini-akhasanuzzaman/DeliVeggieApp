namespace ProductService.Extensions.Translators
{
    using Application.Common.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using System;
    using System.Threading.Tasks;

    public static class ExceptionToHttpTranslator
    {
        public static async Task Translate(HttpContext httpContext, Exception exception)
        {
            string errorMessage = null;
            var httpResponse = httpContext.Response;
            httpResponse.Headers.Add("Exception-Type", exception.GetType().Name);

            if (exception is ProductServiceBaseException productServiceBaseException)
            {
                httpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = productServiceBaseException.Reason;
                errorMessage = productServiceBaseException.Reason;
            }

            httpResponse.StatusCode = MapExceptionToStatusCode(exception);
            await httpResponse.WriteAsync(errorMessage);
        }

        private static int MapExceptionToStatusCode(Exception exception)
        {
            if (exception is ProductServiceNotFoundBaseException)
            {
                return 404;
            }
            else if (exception is ProductServiceBusinessBaseException)
            {
                return 400;
            }

            return 500;
        }
    }
}
