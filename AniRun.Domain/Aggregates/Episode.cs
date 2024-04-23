using AniRun.Domain.Base;

namespace AniRun.Domain.Aggregates;

public class Episode : BaseRecord
{
    public string Name { get; set; }
    public Media Video { get; set; }
    public int Number { get; set; }
    public Title Title { get; set; }
    public Guid TitleId { get; set; }
}