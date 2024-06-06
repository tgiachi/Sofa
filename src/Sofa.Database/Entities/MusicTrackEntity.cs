using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("musics"), Index(nameof(Genres)), Index(nameof(Title))]
public class MusicTrackEntity : AbstractBaseEntity
{
    [MaxLength(200)] public string Hash { get; set; }

    [MaxLength(500)] public string FileName { get; set; }

    [MaxLength(200)] public string Title { get; set; }

    public Guid AlbumId { get; set; }

    public MusicAlbumEntity Album { get; set; }

    public Guid ArtistId { get; set; }

    public MusicArtistEntity Artist { get; set; }

    public string[]? Genres { get; set; }

    public TimeSpan Duration { get; set; }

    public long Size { get; set; }

    public int BitRate { get; set; }

    public int TrackNumber { get; set; }

    public int DiscNumber { get; set; }
}
