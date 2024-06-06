using Sofa.Core.Data.Messages;
using Sofa.Database.Entities;
using Sofa.Database.Interfaces;
using TagLib;

namespace Sofa.Engine.Services;

public class MediaService
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ILogger _logger;

    private readonly IDataAccess<MusicArtistEntity> _artistDao;

    private readonly IDataAccess<MusicAlbumEntity> _albumDao;

    private readonly IDataAccess<MusicTrackEntity> _trackDao;

    public MediaService(
        ILogger<MediaService> logger, IDataAccess<MusicArtistEntity> artistDao, IDataAccess<MusicAlbumEntity> albumDao,
        IDataAccess<MusicTrackEntity> trackDao
    )
    {
        _logger = logger;
        _artistDao = artistDao;
        _albumDao = albumDao;
        _trackDao = trackDao;
    }

    public async Task AddMedia(MediaAddedEvent media)
    {
        await _semaphore.WaitAsync();

        using var tagFile = TagLib.File.Create(media.FileName);
        var tag = tagFile.Tag;

        


        _semaphore.Release();
    }
}
