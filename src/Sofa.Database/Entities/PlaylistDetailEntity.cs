using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("playlist_details")]
public class PlaylistDetailEntity : AbstractBaseEntity
{
    public Guid PlaylistId { get; set; }

    public PlaylistEntity Playlist { get; set; }

    public Guid MusicTrackId { get; set; }

    public MusicTrackEntity MusicTrack { get; set; }
}
