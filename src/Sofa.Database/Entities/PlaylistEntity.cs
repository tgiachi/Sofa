using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("playlists")]
public class PlaylistEntity : AbstractBaseEntity
{
    public Guid UserId { get; set; }

    public UserEntity User { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsPublic { get; set; }

    public List<PlaylistDetailEntity> PlaylistDetails { get; set; }

    public int TrackCount => PlaylistDetails.Count;
}
