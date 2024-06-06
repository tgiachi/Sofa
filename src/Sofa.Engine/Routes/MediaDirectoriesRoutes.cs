using Microsoft.AspNetCore.Mvc;
using Sofa.Database.Entities;
using Sofa.Engine.Requests;
using Sofa.Engine.Services;

namespace Sofa.Engine.Routes;

public static class MediaDirectoriesRoutes
{
    public static IEndpointRouteBuilder MapMediaDirectoriesRoutes(this WebApplication webApplication)
    {
        var group = webApplication.MapGroup("/directories");


        group.MapPost(
            "/add",
            async ([FromServices] MediaDirectoriesService mediaDirectoriesService, [FromBody] AddMediaPathRequest entity) =>
            {
                var result = await mediaDirectoriesService.AddMediaDirectory(
                    new MusicPathEntity
                    {
                        Name = entity.Name,
                        Path = entity.Path,
                        AutoScan = entity.AutoScan,
                        IsEnabled = true
                    },
                    CancellationToken.None
                );

                return result == null ? Results.BadRequest() : Results.Ok(result);
            }
        );

        group.MapGet(
            "/get",
            async ([FromServices] MediaDirectoriesService mediaDirectoriesService) =>
            {
                var result = await mediaDirectoriesService.GetMediaDirectories(CancellationToken.None);
                return Results.Ok(result);
            }
        );

        group.MapPost(
            "/scan/{id:guid}",
            (Guid id, [FromServices] MediaScannerService scannerService) =>
            {
                Task.Run(() => scannerService.ScanDirectoryAsync(id));

                return Results.Ok();
            }
        );


        return group;
    }
}
