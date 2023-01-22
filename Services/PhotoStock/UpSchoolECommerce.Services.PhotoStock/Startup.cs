using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpSchoolECommerce.Services.PhotoStock
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

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //burada b�t�n mikroservislerimi identity ile haberle�tirece�im.
                options.Authority = Configuration["IdentityServerURL"];//buraya appsettings.json i�inde tan�mlad���m�z IdentityServerUrl keyini veriyoruz.Authority identity server ile bulundu�umuz katalog servisini ba�l�yor. Girecek ki�inin bilgi kontrol�n� sa�lay�p o sayfaya y�nlendirecek
                options.Audience = "Resources_PhotoStock"; //�uanda katalog mikroservis yap�land�rmas�n� yazd���m i�in onun resource'�n� yazaca��m
                options.RequireHttpsMetadata = false;
                //bu komut https gerekli de�il http ile de ba�lant� sa�layabilecek


            });

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());//proje seviyesinde authorize uyguluyor
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpSchoolECommerce.Services.PhotoStock", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpSchoolECommerce.Services.PhotoStock v1"));
            }

            app.UseStaticFiles();//wwwroot kullan�lacak
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
