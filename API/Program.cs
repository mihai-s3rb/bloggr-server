using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Infrastructure;
using Bloggr.Infrastructure.Repositories;
using Bloggr.WebUI.Extensions;
using Bloggr.WebUI.Hubs;
using Domain.Abstracts;
using FluentValidation;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBaseServices();

builder.ConfigureIdentiy();

builder.ConfigureJWT(builder.Configuration);

builder.AddSwaggerCustom();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");


app.UseAuthentication();
app.UseAuthorization();

app.ConfigureCustomExceptionHandler();

app.MapHub<ChatHub>("/hub");
app.MapControllers();

app.Run();
