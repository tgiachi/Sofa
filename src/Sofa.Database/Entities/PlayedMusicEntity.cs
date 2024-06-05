using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("played_musics")]
public class PlayedMusicEntity : AbstractBaseEntity
{
    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public Guid MusicTrackId { get; set; }

    public MusicTrackEntity MusicTrack { get; set; }

    public DateTime PlayedAt { get; set; }

}
