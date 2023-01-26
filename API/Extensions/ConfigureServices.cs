using Bloggr.Infrastructure.Repositories;
using Infrastructure.Context;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Bloggr.Application.Validators.Post;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.WebUI.Filters;
using Bloggr.Infrastructure.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Services;
using Microsoft.Extensions.Options;
using Bloggr.WebUI.Services.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Bloggr.WebUI.Extensions
{
    public static class ConfigureServices
    {
        public static void ConfigureBaseServices(this WebApplicationBuilder builder)
        {
            string policyName = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            ///WEEEOEE signalR X_X
            builder.Services.AddSignalR();

            // Add services to the container.

            builder.Services.AddControllers(options => {
                options.Filters.Add<ValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                }).AddJsonOptions(x => {
                                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                                x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                            });
            //add BloggrContext to API
            builder.Services.AddDbContext<BloggrContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => {
                        b.MigrationsAssembly("Bloggr.Infrastructure");
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        b.CommandTimeout(120);
                });
            });
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //builder.Services.AddValidatorsFromAssemblyContaining<PostValidator>();
            builder.AddVaidators();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IAuthManager), typeof(AuthManager));
            builder.Services.AddScoped(typeof(ICustomAuthorizationHandler), typeof(CustomAuthorizationHandler));
            builder.Services.AddAutoMapper(typeof(Program));

            //ADD USER ACCESSOR
            builder.Services.AddTransient<IUserAccessor, UserAccessor>();

            builder.Services.AddTransient<IAzureStorage, AzureStorage>();

            var assembly = AppDomain.CurrentDomain.Load("Bloggr.Application");
            builder.Services.AddMediatR(assembly);
            builder.Services.AddMediatR(typeof(Program));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        }
        public static void ConfigureIdentiy(this WebApplicationBuilder builder)
        {
            // IDENTITY
            builder.Services.AddIdentity<User, IdentityRole<int>>(q => q.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<BloggrContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this WebApplicationBuilder builder, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");

            builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                    opts.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization(opts =>
            {
                opts.AddPolicy("EditPolicy", policy =>
                    policy.Requirements.Add(new SameAuthorRequirement()));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationHandler>();
        }
        public static void AddSwaggerCustom(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
                    Description = "Bearer [space] add your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "0auth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
