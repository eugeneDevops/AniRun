using AniRun.Domain.Base;

namespace AniRun.Domain.Aggregates;

public class Media : BaseRecord
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
}