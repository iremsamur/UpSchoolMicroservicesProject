using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Catalog.Services.Abstract;
using UpSchoolECommerce.Services.Catalog.Services.Concrete;
using UpSchoolECommerce.Services.Catalog.Settings;

namespace UpSchoolECommerce.Services.Catalog
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
            //jwt bearer paketini burada import ediyoruz.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //burada b�t�n mikroservislerimi identity ile haberle�tirece�im.
                options.Authority = Configuration["IdentityServerURL"];//buraya appsettings.json i�inde tan�mlad���m�z IdentityServerUrl keyini veriyoruz.Authority identity server ile bulundu�umuz katalog servisini ba�l�yor. Girecek ki�inin bilgi kontrol�n� sa�lay�p o sayfaya y�nlendirecek
                options.Audience = "Resources_Catalog"; //�uanda katalog mikroservis yap�land�rmas�n� yazd���m i�in onun resource'�n� yazaca��m
                options.RequireHttpsMetadata = false;
                //bu komut https gerekli de�il http ile de ba�lant� sa�layabilecek


            });
            //dependency injection i�in soyut ve somut servis s�n�f e�le�tirmesini yaz�yoruz
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            //automapper konfig�rasyonu
            services.AddAutoMapper(typeof(Startup));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));//veritaban� ba�lant� bilgilerini yazd���m�z DatabaseSettings key de�eri
            //IDatabaseSettings i�indeki ba�lant� i�in DatabaseSettings.value ile onun de�erlerini al�yoruz
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            });

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());//her sayfada tek tek authorize kullanmadan tek burada bunu ekleyerek konfig�rasyonu sa�l�yor.
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpSchoolECommerce.Services.Catalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpSchoolECommerce.Services.Catalog v1"));
            }

            app.UseRouting();

            app.UseAuthentication();//yetki kontrol� i�in bunuda ekliyoruz

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
