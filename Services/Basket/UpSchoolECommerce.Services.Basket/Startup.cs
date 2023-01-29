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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using UpSchoolECommerce.Services.Basket.Services;
using UpSchoolECommerce.Services.Basket.Settings;
using UpSchoolECommerce.Shared.Services;

namespace UpSchoolECommerce.Services.Basket
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpSchoolECommerce.Services.Basket", Version = "v1" });
            });

            //Redis ayarlar�
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));//Burada GetSection i�ine appsettings.json i�inde verdi�imiz key de�erini veriyoruz

            //�imdi burada RedisService'i tan�mlayal�m.
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                var redis = new RedisService(redisSettings.Host,redisSettings.Port);
                redis.Connect();
                return redis;
            });

            //Token'� koruma alt�na almak i�in a�a��daki sat�r� t�m servislere ekliyoruz. B�ylece her serviste token'� koruma alt�na alm�� oluyoruz
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
        
                options.Authority = Configuration["IdentityServerURL"];//
                options.Audience = "Resources_Basket"; 
                options.RequireHttpsMetadata = false;
               


            });

            //kullan�c� bilgilerini getiren htttpcontextaccessor ve ISharedIdentityServi,ce konfig�rasyonlar�
            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IBasketService, BasketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpSchoolECommerce.Services.Basket v1"));
            }

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
