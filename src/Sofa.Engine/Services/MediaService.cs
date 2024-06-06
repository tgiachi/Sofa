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

    public async Task AddMediaAsync(MediaAddedEvent media)
    {
        await _semaphore.WaitAsync();

        Guid artistId;
        Guid albumId;

        using var tagFile = TagLib.File.Create(media.FileName);
        var tag = tagFile.Tag;

        var artist = tag.FirstAlbumArtist;

        if (await _artistDao.CountAsync(entity => entity.Name == artist) == 0)
        {
            artistId = Guid.NewGuid();
            await _artistDao.AddAsync(new MusicArtistEntity { Id = artistId, Name = artist });
        }
        else
        {
            artistId = (await _artistDao.FirstOrDefaultAsync(entity => entity.Name == artist)).Id;
        }

        if (await _albumDao.CountAsync(entity => entity.Name == tag.Album && entity.ArtistId == artistId) == 0)
        {
            albumId =
                (await _albumDao.AddAsync(new MusicAlbumEntity { Name = tag.Album, ArtistId = artistId })).Id;
        }
        else
        {
            albumId = (await _albumDao.FirstOrDefaultAsync(entity => entity.Name == tag.Album)).Id;
        }

        var track = new MusicTrackEntity
        {
            Id = Guid.NewGuid(),
            Hash = media.Hash,
            AlbumId = albumId,
            ArtistId = artistId,
            Title = tag.Title,
            TrackNumber = (int)tag.Track,
            Duration = tagFile.Properties.Duration,
            FileName = media.FileName,
            Size = new FileInfo(media.FileName).Length,
            BitRate = tagFile.Properties.AudioBitrate
        };

        await _trackDao.AddAsync(track);

        _logger.LogInformation(
            "Added new track:{Artist} - {Album} - {Track} to database",
            tag.FirstAlbumArtist,
            tag.Album,
            tag.Title
        );


        _semaphore.Release();
    }
}
