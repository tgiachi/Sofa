using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sofa.Core.Interfaces.Entities;
using Sofa.Database.Context;
using Sofa.Database.Interfaces;

namespace Sofa.Database.Impl.Base;

public class AbstractDataAccess<TEntity> : IDataAccess<TEntity> where TEntity : class, IBaseDbEntity
{
    private readonly IDbContextFactory<SofaDbContext> _dbContextFactory;
    private readonly ILogger _logger;

    public AbstractDataAccess(
        ILogger<AbstractDataAccess<TEntity>> logger, IDbContextFactory<SofaDbContext> dbContextFactory
    )
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Adding entity {Entity}", entity.GetType().Name);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Updating entity {Entity}", entity.GetType().Name);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        dbContext.Set<TEntity>().Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Deleting entity {Entity}", entity.GetType().Name);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        dbContext.Set<TEntity>().Remove(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting entity by id {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting all entities");
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<bool> IsExists(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Checking entity exists by id {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> IsExists(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default
    )
    {
        _logger.LogDebug("Checking entity exists by expression {Expression}", expression);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> QueryAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default
    )
    {
        _logger.LogDebug("Querying entity by expression {Expression}", expression);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default
    )
    {
        _logger.LogDebug("Getting first or default entity by expression {Expression}", expression);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Counting entities");
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Set<TEntity>().LongCountAsync(cancellationToken);
    }
}
