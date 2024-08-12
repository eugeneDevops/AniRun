namespace AniRun.Application.Models.FormModels;

public class FormMedia
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[]? FileBytes { get; set; }

}