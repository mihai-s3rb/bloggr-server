using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Application.Validators.Posts;
using Bloggr.Domain.Interfaces;
using Bloggr.Infrastructure;
using Bloggr.Infrastructure.Repositories;
using Bloggr.WebUI.Extensions;
using Domain.Abstracts;
using FluentValidation;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ConfigureCustomExceptionHandler();

app.MapControllers();

app.Run();
