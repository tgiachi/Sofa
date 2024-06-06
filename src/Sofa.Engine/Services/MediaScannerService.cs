using System.Diagnostics;
using Sofa.Core.Data.Messages;
using Sofa.Core.Utils;
using Sofa.Database.Entities;
using Sofa.Database.Interfaces;
using Sofa.Engine.Services.Queues;

namespace Sofa.Engine.Services;

public class MediaScannerService
{
    private readonly ILogger _logger;

    private readonly IDataAccess<MusicPathEntity> _dataAccess;

    private readonly MediaScannerQueueService _mediaScannerQueueService;

    public MediaScannerService(
        ILogger<MediaScannerService> logger, IDataAccess<MusicPathEntity> dataAccess,
        MediaScannerQueueService mediaScannerQueueService
    )
    {
        _logger = logger;
        _dataAccess = dataAccess;
        _mediaScannerQueueService = mediaScannerQueueService;
    }

    public async Task ScanDirectoryAsync(Guid id)
    {
        var directory = await _dataAccess.GetByIdAsync(id, CancellationToken.None);

        if (directory == null)
        {
            _logger.LogWarning("Directory not found");
            return;
        }

        if (!Directory.Exists(directory.Path))
        {
            _logger.LogWarning("Directory not found");
            return;
        }

        var sw = Stopwatch.StartNew();
        var files = Directory.GetFiles(directory.Path, "*.*", SearchOption.AllDirectories);

        _logger.LogInformation("Found {Count} files in {Elapsed}ms", files.Length, sw.ElapsedMilliseconds);

        Parallel.ForEachAsync(
            files,
            async (file, token) =>
            {
                var hash = HashingUtils.GetFileHash(file);
                var mediaAdded = new MediaAddedEvent(file, hash, Path.GetExtension(file));

                await _mediaScannerQueueService.EnqueueAsync(mediaAdded, token);
            }
        );
    }
}
