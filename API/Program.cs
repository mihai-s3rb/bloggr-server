using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Interfaces;
using Bloggr.Infrastructure.Repositories;
using Domain.Abstracts;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//add BloggrContext to API


builder.Services.AddDbContext<BloggrContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Bloggr.Infrastructure"));
});
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
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

app.MapControllers();

app.Run();
