using System.Linq.Expressions;
using AniRun.Domain.Base;
using AniRun.DomainServices.Base;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniRun.Persistence.Repositories;

public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseRecord
{
    private readonly AniDbContext _context;
    private DbSet<TEntity> Entities => _context.Set<TEntity>();

    public EntityRepository(AniDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TEntity>> FindAll(CancellationToken cancellationToken = default)
    {
        return await Entities.AsQueryable().ToListAsync(cancellationToken);
    }
    
    public async Task<List<TEntity>> FindAll(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await Entities.WithSpecification(spec).AsQueryable().ToListAsync(cancellationToken);
    }
    
    public async Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FirstOrDefaultAsync(en => en.Id == id, cancellationToken);
        return entity;
    }
    
    public async Task<TEntity> FindById(Guid id, Specification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.WithSpecification(spec).FirstOrDefaultAsync(en => en.Id == id, cancellationToken);
        return entity;
    }
    
    public async Task<List<TEntity>> FindByIds(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = await Entities.Where(entity => ids.Contains(entity.Id)).ToListAsync(cancellationToken);
        return entities;
    }
    
    public async Task<List<TEntity>> FindByIds(List<Guid> ids, Specification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var entities = await Entities.WithSpecification(spec).Where(entity => ids.Contains(entity.Id)).ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<TEntity> AddAsnyc(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityDb = await Entities.AddAsync(entity, cancellationToken);
        return entityDb.Entity;
    }

    public async Task<TEntity> UpdateAsnyc(Guid id, TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityDb = await Entities.FirstOrDefaultAsync(en => en.Id == id, cancellationToken);
        entityDb = entity;
        entityDb = Entities.Update(entityDb).Entity;
        return entityDb;
    }

    public async Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FirstOrDefaultAsync(en => en.Id == id, cancellationToken);
        entity = Entities.Remove(entity).Entity;
        return entity;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var dtNow = DateTimeOffset.UtcNow;
        
        foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            if (entry.Entity is BaseRecord)
            {
                var entity = entry.Entity as BaseRecord;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = entity.EditedAt = dtNow;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entity.EditedAt = dtNow;
                }
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}