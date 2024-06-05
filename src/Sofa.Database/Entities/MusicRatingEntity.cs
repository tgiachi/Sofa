using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("music_ratings")]
public class MusicRatingEntity : AbstractBaseEntity
{
    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public Guid MusicId { get; set; }

    public MusicTrackEntity MusicTrack { get; set; }

    public int Rate { get; set; }
}
