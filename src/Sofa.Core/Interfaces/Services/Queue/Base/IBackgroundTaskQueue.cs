namespace Sofa.Core.Interfaces.Services.Queue.Base;

public interface IBackgroundTaskQueue<in TEntity> where TEntity : class
{
    ValueTask EnqueueAsync(TEntity entity, CancellationToken cancellationToken = default);
}
