using AniRun.Application.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace AniRun.Application.Services;

public interface IMediaService
{
    Task<ViewMedia> UploadMedia(IFormFile file, CancellationToken cancellationToken = default);
    Task<ViewMedia> GetMedia(Guid id, CancellationToken cancellationToken = default);
    Task<ViewMedia> DeleteMedia(Guid id, CancellationToken cancellationToken = default);
}