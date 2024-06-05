using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("music_paths")]
public class MusicPathEntity : AbstractBaseEntity
{
    [MaxLength(200)] public string Name { get; set; }
    [MaxLength(300)] public string Path { get; set; }

    public bool IsEnabled { get; set; }

    public bool AutoScan { get; set; }

    public DateTime? LastScan { get; set; }
}
