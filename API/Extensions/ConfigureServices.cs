using Bloggr.Application.Validators.Posts;
using Bloggr.Domain.Interfaces;
using Bloggr.Infrastructure.Repositories;
using Bloggr.Infrastructure;
using Infrastructure.Context;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

namespace Bloggr.WebUI.Extensions
{
    public static class ConfigureServices
    {
        public static void ConfigureCustomServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //add BloggrContext to API


            builder.Services.AddDbContext<BloggrContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Bloggr.Infrastructure"));
            });
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddValidatorsFromAssemblyContaining<PostValidator>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(Program));

            var assembly = AppDomain.CurrentDomain.Load("Bloggr.Application");
            builder.Services.AddMediatR(assembly);
            builder.Services.AddMediatR(typeof(Program));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
