using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using TestApp.Core.Dtos.Options;
using TestApp.Core.Entities.Identity;
using TestApp.Middleware;
using TestApp.Persistent.Contexts;
using TestApp.Services.Mappers;

namespace TestApp.Extentions
{
    public static  class StartupExtensions
    {

        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(options => configuration.GetSection("jwt").Bind(options));
            services.Configure<SwaggerOptions>(options => configuration.GetSection("Swagger").Bind(options));
            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration, string xmlPath)
        {
            SwaggerOptions temp = new SwaggerOptions();
            configuration.GetSection("Swagger").Bind(temp);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = temp.Version, Title = temp.Title });

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } } };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" }
                        }, new List<string>() }

                });

                c.IncludeXmlComments(xmlPath);

            });

            return services;
        }


        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                        .GetSection("RequireDigit").Value);
                    options.Password.RequireLowercase = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                        .GetSection("RequireLowercase").Value);
                    options.Password.RequireUppercase = Boolean.Parse(configuration.GetSection("IdentityConfiguration")
                        .GetSection("RequireUppercase").Value);
                    options.Password.RequireNonAlphanumeric = Boolean.Parse(configuration
                        .GetSection("IdentityConfiguration").GetSection("RequireNonAlphanumeric").Value);
                    options.Password.RequiredLength = int.Parse(configuration.GetSection("IdentityConfiguration")
                        .GetSection("RequiredLength").Value);
                })
                .AddEntityFrameworkStores<TestAppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }



        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TestAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TestAppConnection"));
            });

            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }


        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            SwaggerOptions temp = new SwaggerOptions();
            configuration.GetSection("Swagger").Bind(temp);
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", temp.Name);
                c.DefaultModelRendering(ModelRendering.Model);
                c.EnableDeepLinking();
                c.DocumentTitle = temp.Title;

            });
            app.UseSwagger();
            return app;
        }
        public static IServiceCollection AutoServiceRegister(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses()
                    .AsMatchingInterface()
                    .WithScopedLifetime());

            return services;
        }


        public static IServiceCollection ManualServiceRegister(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var optionsw = new JwtOptions();

            var section = configuration.GetSection("jwt");
            section.Bind(optionsw);
            services.Configure<JwtOptions>(section);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = optionsw.Audience,
                    ValidIssuer = optionsw.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionsw.SecretKey))
                };

            });

            return services;

        }


    }
}