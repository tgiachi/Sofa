using System.Threading.Channels;
using MessagePipe;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sofa.Core.Interfaces.Services.Queue.Base;

namespace Sofa.Core.Impl.Queues;

public abstract class AbstractQueueService<TEntity> : BackgroundService, IBackgroundTaskQueue<TEntity> where TEntity : class
{
    private readonly Channel<TEntity> _channel;

    private readonly ILogger _logger;
    private readonly IAsyncPublisher<TEntity> _publisher;


    protected AbstractQueueService(
        int capacity, ILogger<AbstractQueueService<TEntity>> logger, IAsyncPublisher<TEntity> publisher
    )
    {
        _logger = logger;
        _publisher = publisher;
        _logger.LogInformation(
            "Creating a new queue with capacity {Capacity} for type: {TEntity}",
            capacity,
            typeof(TEntity).Name
        );
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<TEntity>(options);
    }

    public ValueTask EnqueueAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Queue message type: {MessageType} size: {Size}",
            typeof(TEntity).Name,
            _channel.Reader.Count
        );

        return _channel.Writer.WriteAsync(entity, cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await _channel.Reader.ReadAsync(stoppingToken);

            _logger.LogInformation(
                "Dequeued message: {Message} message remains: {Messages}",
                message,
                _channel.Reader.Count
            );
            await _publisher.PublishAsync(message, stoppingToken);
        }
    }
}
