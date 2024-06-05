using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("music_artists")]
public class MusicArtistEntity : AbstractBaseEntity
{
    [MaxLength(200)] public string Name { get; set; }
}
