using Microsoft.AspNetCore.Http;

namespace AniRun.Application.Models.FormModels;

public class FormEpisode
{
    public string Name { get; set; }
    public Guid VideoId { get; set; }
    public FormMedia Video { get; set; }
    public int Number { get; set; }
    public Guid TitleId { get; set; }
}