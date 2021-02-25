namespace ProductService
{
    using Application;
    using Application.Model;
    using DeliVeggieSharedLibrary.Model;
    using EasyNetQ;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using ProductService.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("DeliVeggiePolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddApplication();

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.GetSection(nameof(ConfigSection.RabbitMq)).Bind(rabbitMqConfig);

            services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConfig.ConnectionString));
            services.Configure<RabbitMqConfig>(Configuration.GetSection(nameof(ConfigSection.RabbitMq)));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService v1"));
            }

            //app.UseHttpsRedirection();
            app.UseCors("DeliVeggiePolicy");

            app.UseCustomExceptionHandling();
            app.UseRouting(); 

            app
             .UseCors(options => options
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
