using AniRun.Domain.Base;

namespace AniRun.DomainServices.Base;

public interface IEntityRepository<TEntity> where TEntity : BaseRecord
{
    Task<List<TEntity>> FindAll(CancellationToken cancellationToken = default);
    Task<List<TEntity>> FindByIds(List<Guid> ids, CancellationToken cancellationToken = default);
    Task<TEntity> FindById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsnyc(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsnyc(Guid id, TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}