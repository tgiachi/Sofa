using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("music_albums")]
public class MusicAlbumEntity : AbstractBaseEntity
{
    [MaxLength(200)] public string Name { get; set; }


    [MaxLength(500)] public string? Description { get; set; }

    [MaxLength(500)] public string? CoverPath { get; set; }

    public int? ReleaseYear { get; set; }

    public int? TrackCount { get; set; }

    public int DiscCount { get; set; } = 1;

    public Guid ArtistId { get; set; }

    public MusicArtistEntity Artist { get; set; }
}
