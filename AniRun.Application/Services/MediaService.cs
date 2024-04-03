using Amazon.S3;
using Amazon.S3.Model;
using AniRun.Application.Models;
using AniRun.Application.Models.ViewModels;
using AniRun.Domain.Models;
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

    public MediaService(IOptionsMonitor<CredentialsStorage> optionsMonitor, IMediaRepository repository, 
        IMapper mapper)
    {
        var credentialsStorage = optionsMonitor.CurrentValue;
        var credentials = new Amazon.Runtime.BasicAWSCredentials(
            credentialsStorage.AccessKey,
            credentialsStorage.SecretKey
        );
        
        _s3Client = new AmazonS3Client(credentials);
        _bucketName = credentialsStorage.BucketName;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ViewMedia> UploadMedia(IFormFile file, CancellationToken cancellationToken = default)
    {
        var result = new ViewMedia();
        try
        {
            var media = _mapper.Map<IFormFile, Media>(file);
            media = await _repository.AddAsnyc(media, cancellationToken);
            using (var stream = file.OpenReadStream())
            {
                var args = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = media.Id.ToString(),
                    InputStream = stream,
                    ContentType = media.ContentType,
                };
                await _s3Client.PutObjectAsync(args);
                Console.WriteLine("Media uploaded successfully.");
            }
            result = _mapper.Map<Media, ViewMedia>(media);
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
            var media = await _repository.FindById(id);
            
            var args = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = media.Id.ToString(),
            };

            var response = await _s3Client.GetObjectAsync(args);
            result = _mapper.Map<Media, ViewMedia>(media);
            result.FileStream = response.ResponseStream;
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
            await _s3Client.DeleteObjectAsync(args);
            media = await _repository.DeleteAsync(id, cancellationToken);
            Console.WriteLine("Media deleted successfully.");
            result = _mapper.Map<Media, ViewMedia>(media);
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an media", e.Message);
        }

        return result;
    }
}