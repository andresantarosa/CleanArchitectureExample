using CleanArchitectureExample.Application.DomainNotifications;
using CleanArchitectureExample.CrossCutting;
using CleanArchitectureExample.Domain.Core.DomainNotification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace CleanArchitectureExample.WebAPI
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
            services.AddControllers().AddNewtonsoftJson(config => config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
            IoCContainer.InitializeContainer(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp)
        {
            //DomainNotificationsContainer.serviceProvider = app.ApplicationServices;
            ServiceLocator.Initialize(sp.GetService<IContainer>());

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
        }
    }
}
