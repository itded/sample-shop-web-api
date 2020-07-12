using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleShopWebApi.Api.Filters;
using SampleShopWebApi.Api.Settings;
using SampleShopWebApi.Business;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.Data;
using SampleShopWebApi.Data.Repositories;
using SampleShopWebApi.Data.Repositories.Interfaces;

namespace SampleShopWebApi.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// .Ctor
        /// </summary>
        /// <param name="configuration">A set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Set up the DB context
            string connString = Configuration.GetConnectionString("SampleShop");
            services.AddDbContext<ShopDbContext>(options => {
                options.UseSqlServer(connString);
            });

            // DI
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Add API Versioning to as service to your project 
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            services.Configure<ApiControllerSettings>(this.Configuration.GetSection("ApiControllerSettings"));

            services.AddSwaggerGen(c =>
            {
                string name = this.Configuration.GetValue<string>("SwaggerSettings:Name");
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = $"{name} - API 1.0"
                });

                c.SwaggerDoc("v2.0", new OpenApiInfo
                {
                    Version = "v2.0",
                    Title = $"{name} - API 2.0"
                });
                
                // Support for ASP.NET API Versioning
                // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/244#issuecomment-276595734
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc.ActionDescriptor.EndpointMetadata
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return !versions.Any() || versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionInPathsFilter>();

                // Read annotations in API controllers
                c.EnableAnnotations();

                // Set the comments path for the Swagger
                var xmlFile = "SampleShopWebApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)){
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string name = this.Configuration.GetValue<string>("SwaggerSettings:Name");
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", $"{name} - API 1.0");
                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", $"{name} - API 2.0");
            });
        }
    }
}