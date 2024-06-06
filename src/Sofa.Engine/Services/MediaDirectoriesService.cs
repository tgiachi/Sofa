using Sofa.Database.Entities;
using Sofa.Database.Interfaces;

namespace Sofa.Engine.Services;

public class MediaDirectoriesService
{
    private readonly ILogger _logger;

    private readonly IDataAccess<MusicPathEntity> _dataAccess;

    public MediaDirectoriesService(ILogger<MediaDirectoriesService> logger, IDataAccess<MusicPathEntity> dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<MusicPathEntity>> GetMediaDirectories(CancellationToken cancellationToken) =>
        await _dataAccess.GetAllAsync(cancellationToken);


    public async Task<MusicPathEntity?> AddMediaDirectory(MusicPathEntity entity, CancellationToken cancellationToken)
    {
        var exist = await _dataAccess.QueryAsync(x => x.Path == entity.Path, cancellationToken);

        if (exist.Any())
        {
            _logger.LogWarning("Path already exists");
            return null;
        }

        return await _dataAccess.AddAsync(entity, cancellationToken);
    }
}
