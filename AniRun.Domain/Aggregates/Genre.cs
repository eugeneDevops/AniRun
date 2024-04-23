using AniRun.Domain.Base;

namespace AniRun.Domain.Aggregates;

public class Genre : BaseRecord
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Title> Titles { get; set; }
}