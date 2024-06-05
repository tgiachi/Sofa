using Microsoft.EntityFrameworkCore;
using Serilog;
using Sofa.Core.Extensions;
using Sofa.Database.Context;
using Sofa.Database.Modules;

namespace Sofa.Engine;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        builder.Services.AddPooledDbContextFactory<SofaDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlite("Data Source=/tmp/sofa.db")
        );

        builder.Services.AddModule<DatabaseModule>();

        // Add services to the container.
        builder.Services.AddAuthorization();
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

        var summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        app.MapGet(
                "/weatherforecast",
                (HttpContext httpContext) =>
                {
                    var forecast = Enumerable
                        .Range(1, 5)
                        .Select(index => new WeatherForecast
                        {
                            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            TemperatureC = Random.Shared.Next(-20, 55),
                            Summary = summaries[Random.Shared.Next(summaries.Length)]
                        })
                        .ToArray();
                    return forecast;
                }
            )
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        Log.Logger.Information("Performing db migration");

        using (var scope = app.Services.CreateScope())
        {
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SofaDbContext>>();
            using var context = factory.CreateDbContext();
            context.Database.Migrate();
        }

        app.Run();
    }
}
