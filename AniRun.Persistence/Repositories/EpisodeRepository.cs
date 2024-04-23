using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;

namespace AniRun.Persistence.Repositories;

public class EpisodeRepository : EntityRepository<Episode>, IEpisodeRepository
{
    public EpisodeRepository(AniDbContext context) : base(context)
    {
    }
}