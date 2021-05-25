using ApiRestNorthwind.Token_Bearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestNorthwind
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
            // Aqui es donde se configura Token Bearer JWT
            services.AddSingleton<IAuthorizationHandler, MyApiHandler>();
            // Se especifica los esquemas de autoridad A y B 
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("SchemeStsA", options =>
                {
                    options.Audience = "ProtectedApiResourceA";
                    options.Authority = "https://localhost:44318";
                })
                .AddJwtBearer("SchemeStsB", options =>
                {
                    options.Audience = "ProtectedApiResourceB";
                    options.Authority = "https://localhost:44367";
                });

            // Se agregan los esquemas de autoridad para crear una poliza
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("SchemeStsA", "SchemeStsB")
                    .Build();

                // Se agrega la poliza 
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AddRequirements(new MyApiRequirement());
                });
            });

            services.AddControllers();
            services.AddOptions();
            services.AddSwaggerGen(c =>
            {
                // agregamos una UI para usar el Token Bearer JWT en Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", 
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
            {securityScheme, new string[] { }}
             });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "An API ",
                    Version = "v1",

                });

            });

            // Agregamos una poliza de CORS llamada MyPolicy usada en cada controlador
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRestNorthwind v1"));
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API One");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();  
            // Usamos la poliza CORS creada
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
