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
                //burada bütün mikroservislerimi identity ile haberleþtireceðim.
                options.Authority = Configuration["IdentityServerURL"];//buraya appsettings.json içinde tanýmladýðýmýz IdentityServerUrl keyini veriyoruz.Authority identity server ile bulunduðumuz katalog servisini baðlýyor. Girecek kiþinin bilgi kontrolünü saðlayýp o sayfaya yönlendirecek
                options.Audience = "Resources_Catalog"; //þuanda katalog mikroservis yapýlandýrmasýný yazdýðým için onun resource'ünü yazacaðým
                options.RequireHttpsMetadata = false;
                //bu komut https gerekli deðil http ile de baðlantý saðlayabilecek


            });
            //dependency injection için soyut ve somut servis sýnýf eþleþtirmesini yazýyoruz
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            //automapper konfigürasyonu
            services.AddAutoMapper(typeof(Startup));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));//veritabaný baðlantý bilgilerini yazdýðýmýz DatabaseSettings key deðeri
            //IDatabaseSettings içindeki baðlantý için DatabaseSettings.value ile onun deðerlerini alýyoruz
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            });

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());//her sayfada tek tek authorize kullanmadan tek burada bunu ekleyerek konfigürasyonu saðlýyor.
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

            app.UseAuthentication();//yetki kontrolü için bunuda ekliyoruz

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
