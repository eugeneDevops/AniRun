namespace AniRun.Application.Models.ViewModels;

public class ViewMedia
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public Stream? FileStream { get; set; }
}