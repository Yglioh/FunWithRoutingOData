using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;    Needed when using env.IsDevelopment()
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Models.V1;
using Repositories.V1;
using System.Text.Json.Serialization;

namespace OData8x
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())) // Enable string representation of Enums in schema and requests
                .AddOData(options =>
                                {
                                    options.AddRouteComponents("odata", GetEdmModel());
                                    options.Filter().OrderBy().Expand().Select().Count().SetMaxTop(null); // Enable specific OData query options for requests
                                }
                            );

            // Repositories
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IProcessFlowRepository, ProcessFlowRepository>();
            services.AddSingleton<IParameterRepository, ParameterRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseODataRouteDebug(); // Give access to http://localhost:5000/$odata   All listed endpoints are case insensitive
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This extension maps/exposes controller action methods to endpoints
                                            // Customizations happen now only in the service configuration itself as oposed to OData 7.x
            });
        }

        // ToDo: this can be rewritten via DI (e.g. ApiVersioning https://github.com/dotnet/aspnet-api-versioning)
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();

            builder.EntitySet<Project>("Projects");
            builder.EntitySet<ProcessFlow>("ProcessFlows"); // Contained entities are discovered and added automatically. Name must be the same as the corresponding controller

            // Defining inheritance is necessary when serializing response content to include derived type instead of base type
            // Response body will contain automatically '@odata.type'
            builder.EntityType<TextParameter>().DerivesFrom<Parameter>();
            builder.EntityType<NumberParameter>().DerivesFrom<Parameter>();
            builder.EntityType<CheckBoxParameter>().DerivesFrom<Parameter>();
            builder.EntityType<DateParameter>().DerivesFrom<Parameter>();

            return builder.GetEdmModel();
        }
    }
}
