using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotasService.Middleware;
using NotasService.Models;
using NotasService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService
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
            services.AddCors();
            services.AddMvc();

            var jwtTokenConfig = new JwtTokenConfig();
            jwtTokenConfig.Secret = "xecretKeywqejane";
            jwtTokenConfig.Audience = "https://Summit.com.co";
            jwtTokenConfig.Issuer = "https://Summit.com.co";
            jwtTokenConfig.AccessTokenExpiration = 10;
            jwtTokenConfig.RefreshTokenExpiration = 50;

            services.AddSingleton(jwtTokenConfig);

            services.AddTransient<INotasAddService, NotasAddService>();
            services.AddTransient<INotasGetService, NotasGetService>();
            services.AddTransient<INotasUpdateService, NotasUpdateService>();
            services.AddTransient<INotasDeleteService, NotasDeleteService>();
            services.AddTransient<INotasSearchByDateService, NotasSearchByDateService>();
            services.AddTransient<IJwtAuthManager, JwtAuthManager>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            
            //Cors
            services.AddCors(options => options.AddPolicy("AllowWebapp",
                                                 builder => builder.AllowAnyOrigin()
                                                                    .AllowAnyHeader()
                                                                    .AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowWebapp");

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
