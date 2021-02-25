namespace ProductService.Extensions
{
    using Microsoft.AspNetCore.Builder;

    internal static class AppBuilderExtensions
    {
        public static void UseCustomExceptionHandling(this IApplicationBuilder app)
              => app.UseMiddleware<ExceptionHanldingMiddleware>();
    }
}
