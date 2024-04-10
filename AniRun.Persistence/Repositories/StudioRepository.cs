using AniRun.Domain.Models;
using AniRun.DomainServices.Repositories;

namespace AniRun.Persistence.Repositories;

public class StudioRepository : EntityRepository<Studio>, IStudioRepository
{
    public StudioRepository(AniDbContext context) : base(context)
    {
    }
}