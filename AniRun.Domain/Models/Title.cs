using System.ComponentModel.DataAnnotations.Schema;
using AniRun.Domain.Base;
using AniRun.Domain.Enums;

namespace AniRun.Domain.Models;

public class Title : BaseRecord
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeSpan? Duration { get; set; }
    public DateTimeOffset StartDateTitle { get; set; }
    public DateTimeOffset? EndDateTitle { get;set; }
    public Guid? PictureId { get; set; }
    [Column(TypeName = "jsonb")]
    public List<Guid?> Episodes { get; set; } = new List<Guid?>();
    public int? LastEpisode { get; set; }
    
    public TypeTitle Type { get; set; }
    public StatusTitle Status { get; set; }
    public Rating Rating { get; set; }

    public Guid? StudioId { get; set; }
    public Studio? Studio { get; set; }
    
    public List<Genre> Genres { get; set; } = new List<Genre>();
}