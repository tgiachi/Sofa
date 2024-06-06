using MessagePipe;
using Sofa.Core.Data.Messages;

namespace Sofa.Engine.Services;

public class MediaParserService : IHostedService
{
    private readonly ILogger _logger;

    private readonly MediaService _mediaService;

    private readonly IAsyncSubscriber<MediaAddedEvent> _subscriber;

    public MediaParserService(
        ILogger<MediaParserService> logger, IAsyncSubscriber<MediaAddedEvent> subscriber, MediaService mediaService
    )
    {
        _logger = logger;
        _subscriber = subscriber;
        _mediaService = mediaService;
        _subscriber.Subscribe(Handler);
    }

    private async ValueTask Handler(MediaAddedEvent media, CancellationToken arg2)
    {
        if (media.Extension == ".mp3" || media.Extension == ".flac")
        {

            await _mediaService.AddMedia(media);

            return;
        }

        _logger.LogWarning("Unsupported file type: {FileName}", media.FileName);
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
