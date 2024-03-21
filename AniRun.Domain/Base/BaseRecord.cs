namespace AniRun.Domain.Base;

public class BaseRecord
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset EditedAt { get; set; }
}