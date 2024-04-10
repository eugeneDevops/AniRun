namespace AniRun.Application.Models;

public class CredentialsStorage
{ 
    public const string SectionConfiguration = "S3Storage";
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Link { get; set; }
    public string Endpoint { get; set; }
    public string BucketName { get; set; }
}