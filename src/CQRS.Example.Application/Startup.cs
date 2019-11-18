using System.Diagnostics;
using CQRS.Example.Application.Configuration;
using CQRS.Example.Application.Swagger;
using CQRS.Example.Database;
using CQRS.Example.Database.Migrations;
using CQRS.Example.Server;
using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CQRS.Example.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddConfiguredSwagger();
            services.AddApplicationConfiguration(Configuration);
            services.AddMvcCore(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            // Suppress model validation since we do this as part of the decorator chain.
            services.Configure<ApiBehaviorOptions>(o => o.SuppressModelStateInvalidFilter = true);
        }

        public void ConfigureContainer(IServiceContainer container)
        {
            container.RegisterFrom<DatabaseCompositionRoot>();
            container.RegisterFrom<ServerCompositionRoot>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseMigrator databaseMigrator)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            databaseMigrator.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseConfiguredSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
