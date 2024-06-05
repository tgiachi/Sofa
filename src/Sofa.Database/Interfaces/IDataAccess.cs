using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sofa.Core.Interfaces.Entities;

namespace Sofa.Database.Interfaces;

public interface IDataAccess<TEntity> where TEntity : class, IBaseDbEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> IsExists(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsExists(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default
    );

    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default
    );

    Task<long> CountAsync(CancellationToken cancellationToken = default);
}
