using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Shared.Services;
using UpSchoolECommerce.Web.Models;
using UpSchoolECommerce.Web.Services.Abstract;
using UpSchoolECommerce.Web.Services.Concrete;

namespace UpSchoolECommerce.Web
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
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();//token için gerekli
            //mikroservis baðlantýsý için aþaðýdaki konfigürasyonlarý yazýyoruz.
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));//appsettings.json'dan ClientSettings key'ini buraya veriyoruz
             services.Configure<ServicesApiSettings>(Configuration.GetSection("ServicesApiSettings"));//appsettings.json'dan ServicesApiSettings key'ini buraya veriyoruz
            var servicesApiSettings = Configuration.GetSection("ServicesApiSettings").Get<ServicesApiSettings>();
            //Bunu tekrar deðiþkene atayarak yazdýk. Buradaki deðeri aþaðýda kullanmak için deðiþkene atadýk
            //Böylece aþaðýda kullanýp içindeki proplara eriþim saðlarýz.

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddHttpClient<ICategoryService, CategoryService>(opt =>
            {
                opt.BaseAddress = new Uri($"{servicesApiSettings.GatewayBaseUrl}/{servicesApiSettings.Catalog.Path}");//Catalog mikroservisinin url'ýný alýyoruz.
                                                                                                                      //Çünkü http client ile mikroservisteki api metoduna CategoryService içinde istek attýk

            });//Crud iþlemlerine client ile istek atýyoruz.


            services.AddControllersWithViews();
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
            }
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
