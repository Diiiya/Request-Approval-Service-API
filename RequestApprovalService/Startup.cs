using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RequestApprovalService.Bootstrap.Api;

namespace RequestApprovalService
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

            // without DB here 
            services.AddComponents(Configuration.GetConnectionString("RequestsApprovalDatabase"), "UniqKey.*.dll");
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3001")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddSwaggerDocument(c =>
            {
                c.DocumentName = "v1";
                c.PostProcess = d =>
                {
                    var name = Assembly.GetEntryAssembly()?.CustomAttributes
                        .First(x => x.AttributeType == typeof(AssemblyTitleAttribute))
                        .ConstructorArguments.First().Value?.ToString()
                        ?.Split(".").Last();

                    var version = Assembly.GetEntryAssembly()?.CustomAttributes
                        .First(x => x.AttributeType == typeof(AssemblyInformationalVersionAttribute))
                        .ConstructorArguments.First().Value;

                    d.Info.Title = name;
                    d.Info.Version = version as string;
                    d.Info.Description = $"{name} API v1.0";
                };
            });

            services.AddMediatR(typeof(Startup));
            //services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3(settings =>
                {
                    settings.Path = "/api";
                });

                app.UseReDoc(config =>
                {

                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
