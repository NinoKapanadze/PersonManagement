using PersonManagement.API;
using PersonManagement.API.Middlewares;
using PersonManagement.Application;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Infrastructure;
using PersonManagement.Infrastructure.Seeding;

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataContext>();
    var unitOfWork = services.GetRequiredService<IUnitOfWork>();

    await DbSeeder.SeedAsync(unitOfWork, dbContext);
}

var supportedCultures = new[] { "en", "ka"}; 

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
