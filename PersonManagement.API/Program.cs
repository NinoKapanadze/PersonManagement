using Microsoft.EntityFrameworkCore;
using PersonManagement.API;
using PersonManagement.Application;
using PersonManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
        .AddWebApi(configuration)
        .AddApplication(configuration)
        .Addinfrastructure(configuration)
        .AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
