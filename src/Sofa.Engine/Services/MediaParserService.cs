using MessagePipe;
using Sofa.Core.Data.Messages;
using Sofa.Database.Entities;
using Sofa.Database.Interfaces;

namespace Sofa.Engine.Services;

public class MediaParserService : IHostedService
{
    private readonly ILogger _logger;

    private readonly MediaService _mediaService;

    private readonly IDataAccess<MusicTrackEntity> _trackDao;

    private readonly IAsyncSubscriber<MediaAddedEvent> _subscriber;

    public MediaParserService(
        ILogger<MediaParserService> logger, IAsyncSubscriber<MediaAddedEvent> subscriber, MediaService mediaService, IDataAccess<MusicTrackEntity> trackDao
    )
    {
        _logger = logger;
        _subscriber = subscriber;
        _mediaService = mediaService;
        _trackDao = trackDao;
        _subscriber.Subscribe(Handler);
    }

    private async ValueTask Handler(MediaAddedEvent media, CancellationToken arg2)
    {
        if (await _trackDao.CountAsync(x => x.Hash == media.Hash, arg2) > 0)
        {
            return;
        }

        if (media.Extension == ".mp3" || media.Extension == ".flac")
        {
            await _mediaService.AddMediaAsync(media);

            return;
        }

        _logger.LogWarning("Unsupported file type: {FileName}", media.FileName);
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
