using Sofa.Core.Interfaces.Entities;

namespace Sofa.Database.Entities.Base;

public abstract  class AbstractBaseEntity : IBaseDbEntity
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}