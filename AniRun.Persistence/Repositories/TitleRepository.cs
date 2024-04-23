using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;

namespace AniRun.Persistence.Repositories;

public class TitleRepository : EntityRepository<Title>, ITitleRepository
{
    public TitleRepository(AniDbContext context) : base(context)
    {
    }
}