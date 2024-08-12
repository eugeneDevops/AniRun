namespace AniRun.Application.Models.ViewModels;

public class ViewEpisode
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid VideoId { get; set; }
    public ViewMedia Video { get; set; }
    public int Number { get; set; }
    public Guid TitleId { get; set; }
    public ViewTitle Title { get; set; }
}