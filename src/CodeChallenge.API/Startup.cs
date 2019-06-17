using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.API.Infrastructure;
using CodeChallenge.API.Persistence;
using CodeChallenge.API.Services.Abstract;
using CodeChallenge.API.Services.Concrete;
using CodeChallenge.API.Validation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

[assembly: ApiConventionType(typeof(CodeChallengeApiConventions))]

namespace CodeChallenge.API
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
            services.AddDbContext<CodeChallengeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CodeChallengeDb")));

            services.AddScoped<ICompanyService, CompanyService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanyForAddDtoValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CodeChallenge API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\". Get token from POST /Account/Login",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeChallenge API V1");
            });

            app.UseCors(cfg =>
            {
                cfg.AllowAnyOrigin();
                cfg.AllowAnyMethod();
                cfg.AllowAnyHeader();
            });

            app.UseMvc();
        }
    }
}
