using AniRun.Domain.Enums;

namespace AniRun.Application.Models.ViewModels;

public class ViewTitle
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeSpan? Duration { get; set; }
    public DateTimeOffset StartDateTitle { get; set; }
    public DateTimeOffset? EndDateTitle { get;set; }
    public ViewMedia Picture { get; set; }
    public Guid? PictureId { get; set; }
    public int? LastEpisode { get; set; }
    public TypeTitle Type { get; set; }
    public StatusTitle Status { get; set; }
    public Rating Rating { get; set; }

    public Guid? StudioId { get; set; }
    public ViewStudio? Studio { get; set; }
    
    public List<ViewEpisode?> Episodes { get; set; } = new List<ViewEpisode?>();
    public List<ViewGenre> Genres { get; set; } = new List<ViewGenre>();
}