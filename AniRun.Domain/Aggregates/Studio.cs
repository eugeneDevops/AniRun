using AniRun.Domain.Base;

namespace AniRun.Domain.Aggregates;

public class Studio : BaseRecord
{
    public string Name { get; set; }
    public List<Title> Titles { get; set; }
}