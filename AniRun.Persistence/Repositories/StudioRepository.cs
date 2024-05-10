using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;
using Ardalis.Specification;

namespace AniRun.Persistence.Repositories;

public class StudioRepository : EntityRepository<Studio>, IStudioRepository
{
    public StudioRepository(AniDbContext context) : base(context)
    {
    }
}