using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;

namespace AniRun.Persistence.Repositories;

public class GenreRepository : EntityRepository<Genre>, IGenreRepository
{
    public GenreRepository(AniDbContext context) : base(context)
    {
    }
}