using Amazon.S3;
using Amazon.S3.Model;
using AniRun.Application.Models;
using AniRun.Application.Models.FormModels;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Aggregates;
using AniRun.DomainServices.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AniRun.Application.Services;

public class MediaService : IMediaService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly IMediaRepository _repository;
    private readonly IMapper _mapper;
    private readonly string _s3Link;

    public MediaService(IOptionsMonitor<CredentialsStorage> optionsMonitor, IMediaRepository repository, 
        IMapper mapper)
    {
        var credentialsStorage = optionsMonitor.CurrentValue;
        var credentials = new Amazon.Runtime.BasicAWSCredentials(
            credentialsStorage.AccessKey,
            credentialsStorage.SecretKey
        );
        _s3Client = new AmazonS3Client(credentials, new AmazonS3Config()
        {
            ServiceURL = credentialsStorage.Link,
        });
        _s3Link = credentialsStorage.Link;
        _bucketName = credentialsStorage.BucketName;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task CreateBucket(CancellationToken cancellationToken = default)
    {
        var argsBucket = new PutBucketRequest
        {
            BucketName = _bucketName
        };
        await _s3Client.PutBucketAsync(argsBucket, cancellationToken);
    }

    public async Task<ViewMedia> UploadMedia(FormMedia file, CancellationToken cancellationToken = default)
    {
        var result = new ViewMedia();
        try
        {
            var media = _mapper.Map<Media>(file);
            media = await _repository.AddAsnyc(media, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            using (MemoryStream stream = new MemoryStream(file.FileBytes))
            {
                var args = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = media.Id.ToString(),
                    InputStream = stream,
                    ContentType = media.ContentType
                };
                await _s3Client.PutObjectAsync(args, cancellationToken);
                Console.WriteLine("Media uploaded successfully.");
            }
            result = _mapper.Map<ViewMedia>(media);
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when uploading an media", e.Message);
        }
        return result;
    }

    public async Task<ViewMedia> GetMedia(Guid id, CancellationToken cancellationToken = default)
    {
        ViewMedia result = new ViewMedia();
        try
        {
            var media = await _repository.FindById(id, cancellationToken);
            result = _mapper.Map<ViewMedia>(media);
            result.Url = $"{_s3Link}/{_bucketName}/{id}";
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when get an media", e.Message);
            return null;
        }
        return result;
    }

    public async Task<ViewMedia> DeleteMedia(Guid id, CancellationToken cancellationToken = default)
    {
        var result = new ViewMedia();
        try
        {
            var media = await _repository.FindById(id, cancellationToken);
            var args = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = media.Id.ToString()
            };
            await _s3Client.DeleteObjectAsync(args, cancellationToken);
            media = await _repository.DeleteAsync(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            Console.WriteLine("Media deleted successfully.");
            result = _mapper.Map<ViewMedia>(media);
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an media", e.Message);
        }
        return result;
    }

    public string GetUrlMedia(Guid id)
    {
        return $"{_s3Link}/{_bucketName}/{id}";
    }
}