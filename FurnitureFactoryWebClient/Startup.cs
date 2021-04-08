using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogic.BusinessLogic;
using FurnitureFactoryBusinessLogic.Interfaces;
using FurnitureFactoryDatabaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FurnitureFactoryWebClient
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
            services.AddControllersWithViews();
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IPositionLogic, PositionLogic>();
            services.AddTransient<IFurnitureModelLogic, FurnitureModelLogic>();
            services.AddTransient<IFurnitureTypeLogic, FurnitureTypeLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IPaymentLogic, PaymentLogic>();
            services.AddTransient<ReportLogic>();
            services.AddTransient<BackUpAbstractLogic, BackUpLogic>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
