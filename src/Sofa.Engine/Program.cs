using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sofa.Core.Data.Messages;
using Sofa.Core.Extensions;
using Sofa.Database.Context;
using Sofa.Database.Modules;
using Sofa.Engine.Routes;
using Sofa.Engine.Services;
using Sofa.Engine.Services.Queues;

namespace Sofa.Engine;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        builder.Services.AddMessagePipe();

        builder.Services.AddSingleton<MediaService>();
        builder.Services.AddSingleton<MediaScannerQueueService>();
        builder.Services.AddSingleton<MediaScannerService>();
        builder.Services.AddSingleton<MediaParserService>();
        builder.Services.AddHostedService<MediaScannerQueueService>(
            provider => provider.GetRequiredService<MediaScannerQueueService>()
        );

        builder.Services.AddHostedService<MediaParserService>(provider => provider.GetRequiredService<MediaParserService>());


        builder.Services.AddScoped<MediaDirectoriesService>();

        builder.Services.AddPooledDbContextFactory<SofaDbContext>(
            optionsBuilder =>
                optionsBuilder.UseSqlite("Data Source=/tmp/sofa.db")
        );

        builder.Services.AddModule<DatabaseModule>();

        // Add services to the container.
        builder.Services.AddAuthorization();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapMediaDirectoriesRoutes();


        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();

        app.UseAuthorization();


        Log.Logger.Information("Performing db migration");

        using (var scope = app.Services.CreateScope())
        {
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SofaDbContext>>();
            await using var context = await factory.CreateDbContextAsync();
            await context.Database.MigrateAsync();
        }

        await app.RunAsync();
    }
}
