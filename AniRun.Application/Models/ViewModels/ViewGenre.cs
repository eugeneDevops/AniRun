namespace AniRun.Application.Models.ViewModels;

public class ViewGenre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ViewTitle> Titles { get; set; }
}