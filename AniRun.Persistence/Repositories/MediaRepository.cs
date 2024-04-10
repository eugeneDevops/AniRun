using AniRun.Domain.Models;
using AniRun.DomainServices.Repositories;

namespace AniRun.Persistence.Repositories;

public class MediaRepository : EntityRepository<Media>, IMediaRepository
{
    public MediaRepository(AniDbContext context) : base(context)
    {
    }
}