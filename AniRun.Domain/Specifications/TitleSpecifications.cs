using AniRun.Domain.Aggregates;
using Ardalis.Specification;

namespace AniRun.Domain.Specifications;

public class GetTitlesSpecifcation : Specification<Title>
{
    public GetTitlesSpecifcation()
    {
        Query.Include(title => title.Episodes)
            .Include(title => title.Picture)
            .Include(title => title.Studio)
            .Include(title => title.Genres);
    }
}