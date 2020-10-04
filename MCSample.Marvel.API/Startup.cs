using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using External.WebServices.Marvel;
using GraniteCore;
using GraniteCore.AutoMapper;
using GraniteCore.EntityFrameworkCore;
using MCSample.Marvel.DataAccess;
using MCSample.Marvel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MCSample.Marvel.API
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

            addBearerAuthentication(services);

            registerDependancies(services);
        }

        private void addBearerAuthentication(IServiceCollection services)
        {
            // Default scheme
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:65301";

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "marvelAPI");
                });
            });
        }

        private void registerDependancies(IServiceCollection services)
        {
            services.AddGraniteAutoMapper(config =>
            {
                //config.CreateCarMapping();
                //config.CreateCustomerMapping();
            });
            services.AddSingleton(typeof(IBaseRepository<,,>), typeof(MarvelHerosMockRepository<,,>));
            services.AddSingleton(typeof(RestAPIClient), new RestAPIClient());
            services.AddScoped<IAvengersTeamService, AvengersTeamService>();
            services.AddScoped<IHeroService, HeroService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope");
            });
        }
    }
}
