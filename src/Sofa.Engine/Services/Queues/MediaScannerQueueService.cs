using MessagePipe;
using Sofa.Core.Data.Messages;
using Sofa.Core.Impl.Queues;

namespace Sofa.Engine.Services.Queues;

public class MediaScannerQueueService : AbstractQueueService<MediaAddedEvent>
{
    public MediaScannerQueueService(
        ILogger<MediaScannerQueueService> logger, IAsyncPublisher<MediaAddedEvent> publisher
    ) : base(10, logger, publisher)
    {
    }
}
