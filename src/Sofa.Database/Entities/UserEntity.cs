using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sofa.Database.Entities.Base;

namespace Sofa.Database.Entities;

[Table("users")]
public class UserEntity : AbstractBaseEntity
{
    [MaxLength(50)] public string Email { get; set; }

    [MaxLength(200)] public string HashedPassword { get; set; }

    [MaxLength(50)] public string Name { get; set; }

    public bool IsActive { get; set; }
}
