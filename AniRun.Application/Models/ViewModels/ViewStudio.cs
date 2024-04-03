namespace AniRun.Application.Models.ViewModels;

public class ViewStudio
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ViewTitle> Titles { get; set; }
}